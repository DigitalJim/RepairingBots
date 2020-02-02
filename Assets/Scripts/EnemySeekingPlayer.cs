using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySeekingPlayer : MonoBehaviour
{
    public Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemy.enemyMove.enabled = true;
        enemy.enemyMove.goalLocation = EnemyGoal.instance.transform.position;
    }
}
