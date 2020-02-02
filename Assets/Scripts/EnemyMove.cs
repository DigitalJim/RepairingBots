using System.Linq;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public bool doneMoving;

    public Enemy enemy;

    public Vector3 goalLocation;

    public new Rigidbody rigidbody;
    public float maxSpeed = 2;
    public float MaxSpeed => maxSpeed;// enemy.enemySeekingHeals.enabled ? maxSpeed*2: maxSpeed;
    
    float randT;

    float dir = 1;

    private void Start()
    {
        randT = Random.value * 180 * Mathf.Deg2Rad;
    }

    private void FixedUpdate()
    {
        transform.LookAt(goalLocation);

        Vector3 goalVector = goalLocation - transform.position;
        {
            Vector3 velo = rigidbody.velocity;
            if (!doneMoving)
            {
                velo += goalVector;
                foreach (Enemy enemy in Enemy.enemies.Where(e => e != this))
                {
                    Vector3 vector = enemy.transform.position - transform.position;
                    float dist = vector.magnitude;
                    float dot = Vector3.Dot(enemy.enemyMove.rigidbody.velocity.normalized, rigidbody.velocity.normalized);
                    if (dot < 0)
                    {
                        //velo -= (vector * dot) / (dist* dist*9);
                    }
                }
            }

            randT += Random.Range(.5f, 1.5f) * Time.fixedDeltaTime;
            velo += Vector3.Cross(velo, Vector3.up) / 2f * (Mathf.Sin(randT)) * dir * (doneMoving?.25f:1);
            velo.y = 0;
            {
                float mag = velo.magnitude;
                if (mag > MaxSpeed)
                {
                    velo = velo.normalized * MaxSpeed;
                }
            }
            rigidbody.velocity = Vector3.Lerp(velo, rigidbody.velocity, .9f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        dir *= -1;
    }
}