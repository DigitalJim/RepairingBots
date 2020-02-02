﻿using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Enemy enemy;

    public Vector3 goalLocation;

    public new Rigidbody rigidbody;
    public float maxSpeed = 2;
    
    float randT;

    float dir = 1;

    private void Start()
    {
        randT = Random.value * 180 * Mathf.Deg2Rad;
    }

    private void FixedUpdate()
    {
        Vector3 goalVector = goalLocation - transform.position;
        {
            Vector3 velo = (rigidbody.velocity + goalVector);
            velo.y = 0;
            {
                float mag = velo.magnitude;
                if (mag > maxSpeed)
                {
                    velo = velo.normalized * maxSpeed;
                }
            }
            randT += Random.Range(.5f, 1.5f) * Time.fixedDeltaTime;
            velo += Vector3.Cross(velo, Vector3.up)/2f * Mathf.Sin(randT) * dir;
            rigidbody.velocity = Vector3.Lerp(velo, rigidbody.velocity, .9f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        dir *= -1;
    }
}