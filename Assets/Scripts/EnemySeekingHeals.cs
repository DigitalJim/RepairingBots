using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySeekingHeals : MonoBehaviour
{
    public Enemy enemy;

    // Update is called once per frame
    void Update()
    {
        if(enemy.isHealing)
        {

            enemy.enemyMove.enabled = false;
        }
        else
        {
            enemy.enemyMove.enabled = true;
            enemy.enemyMove.goalLocation = RepairArea.repairAreas.Skip(Random.Range(0, RepairArea.repairAreas.Count)).First().transform.position;
        }
    }
}
