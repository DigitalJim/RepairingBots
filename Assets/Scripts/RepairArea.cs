using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RepairArea : MonoBehaviour
{
    public static HashSet<RepairArea> repairAreas = new HashSet<RepairArea>();

    public float range = 2;

    public SphereCollider collider;

    private void Start()
    {
        repairAreas.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.one * range;
        if (Application.isPlaying)
        {
            foreach (Enemy enemy in Enemy.enemies)
            {
                Vector3 relativeEnemyPosition = enemy.transform.position - transform.position;

                enemy.isHealing = relativeEnemyPosition.sqrMagnitude < range * range;
            }
        }
    }

    private void OnDestroy()
    {
        repairAreas.Remove(this);
    }
}
