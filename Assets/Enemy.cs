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
        transform.LookAt(Player.instance.transform);

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
        enemySeekingPlayer.enabled = health == maxHealth;
        enemySeekingHeals.enabled = health != maxHealth;
    }        

    private void OnDestroy()
    {
        enemies.Remove(this);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("PlayerProjectile"))
        {
            PlayerProjectile playerProjectile = collider.gameObject.GetComponent<PlayerProjectile>();
            health -= playerProjectile.GetLifeRemaining();
            UpdateHealthBar();
            if (playerProjectile.deathEffect != null)
            {
                Transform newDeathEffect = Instantiate(playerProjectile.deathEffect, transform.position, default);
                newDeathEffect.localScale = newDeathEffect.localScale * playerProjectile.GetLifeRemaining();
            }
            Destroy(collider.gameObject);

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
