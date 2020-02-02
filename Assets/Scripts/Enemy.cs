using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 5;

    private float health;

    public Image healthbar;

    public static HashSet<Enemy> enemies = new HashSet<Enemy>();

    public float healingTickDelay;
    public float healingPerSecond = 1;
    public bool isHealing;

    public EnemyMove enemyMove;

    public int bulletsOnDeath;
    public PlayerProjectile playerProjectile;

    public EnemySeekingHeals enemySeekingHeals;
    public EnemySeekingPlayer enemySeekingPlayer;

    // Start is called before the first frame update
    void Start()
    {
        enemies.Add(this);
        health = maxHealth;
        UpdateHealthBar();
    }

        float lastHeal;

    // Update is called once per frame
    void Update()
    {
        if (health < maxHealth)
        {
            if (isHealing && lastHeal + healingTickDelay < Time.time)
            {
                lastHeal = Time.time;
                health = Mathf.Clamp(health+ healingPerSecond * healingTickDelay, 0, maxHealth);
            }
        }

        foreach(Enemy enemy in enemies.Where(e=>e!=this))
        {

        }
        ReevaluateBehaviorTree();
    }

    private void UpdateHealthBar()
    {
        healthbar.fillAmount = health / maxHealth;
    }

    public void ReevaluateBehaviorTree()
    {
        bool wantsHeals = health < maxHealth / 2f;
        enemySeekingPlayer.enabled = !wantsHeals;
        enemySeekingHeals.enabled = wantsHeals;
    }        

    private void OnDestroy()
    {
        enemies.Remove(this);
        if (bulletsOnDeath > 0)
        {
            for (int i = 0; i < bulletsOnDeath; i++)
            {
                Vector3 dir = Quaternion.AngleAxis(360 *(i/((float)bulletsOnDeath)), Vector3.up) * Vector3.forward;
                Debug.DrawRay(transform.position, dir, Color.green, 1);
                PlayerProjectile pp = Instantiate<PlayerProjectile>(playerProjectile, transform.position + dir*.2f, default);
                pp.rigidbody.velocity = dir * 4;
                pp.lifetime = .1f;

            }

        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("PlayerProjectile"))
        {
            PlayerProjectile playerProjectile = collider.gameObject.GetComponent<PlayerProjectile>();
            health -= playerProjectile.GetLifeRemaining();
            playerProjectile.lifeMod *= .75f;
            UpdateHealthBar();
            if (playerProjectile.deathEffect != null)
            {
                Transform newDeathEffect = Instantiate(playerProjectile.deathEffect, transform.position, default);
                newDeathEffect.localScale = newDeathEffect.localScale * playerProjectile.GetLifeRemaining();
            }
            //Destroy(collider.gameObject);

            if (health <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                enemySeekingHeals.enabled = true;
            }
        }
    }
}
