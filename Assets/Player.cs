using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public Rigidbody projectile;
    public Transform muzzle;

    private Plane groundPlane = new Plane(Vector3.up, 0);

    public float fireDelay = .1f;

    float lastFired;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Ray cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (groundPlane.Raycast(cursorRay, out float dist))
        {
            transform.LookAt(cursorRay.GetPoint(dist));
        }
        
        if(lastFired + fireDelay < Time.time)
        {
            lastFired = Time.time;
            Rigidbody newProjectile = Instantiate<Rigidbody>(projectile, muzzle.position, default);
            newProjectile.velocity = muzzle.forward * 2;
        }
    }
}
