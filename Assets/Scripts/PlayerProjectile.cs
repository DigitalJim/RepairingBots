using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float lifeMod = 2;
    public float lifetime = 5;
    public Rigidbody rigidbody;

    public Transform deathEffect;

    private Vector3 initalSize;
    private float birthTime;

    public float GetLifeRemaining() =>  (1 - (Time.time - birthTime) / lifetime) * lifeMod;

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
            Destroy(gameObject);
        }
    }
}
