using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float lifeMod = 1;
    public float lifetime = 1;
    public Rigidbody rigidbody;

    public Transform deathEffect;

    private Vector3 initalSize;
    private float birthTime;
    public ParticleSystem particleSystem;

    public float GetLifeRemaining() =>  Mathf.Clamp((1 - (Time.time - birthTime) / lifetime) * lifeMod,0, 999999);

    // Start is called before the first frame update
    void Start()
    {
        birthTime = Time.time;
        initalSize = transform.localScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float lifeRemaining = GetLifeRemaining();
        if (lifeRemaining > 0)
        {
            rigidbody.mass = lifeRemaining;
            rigidbody.transform.localScale = initalSize * lifeRemaining;
        }
        else
        {
            if (particleSystem != null)
            {
                particleSystem.transform.parent = null;
                Destroy(particleSystem.gameObject, 1);
            }
            Destroy(gameObject);
        }
    }
}
