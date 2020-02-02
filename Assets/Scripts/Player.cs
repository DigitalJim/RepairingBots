using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public Rigidbody projectile;
    public Transform muzzle;

    private Plane groundPlane = new Plane(Vector3.up, 0);

    public float fireDelay = .0f;

    public float regenPerSecond = 1f;
    public float regenPerSecondNonFiring = 2f;

    public float RegenPerSecond => Input.GetMouseButton(0) ? regenPerSecond : regenPerSecondNonFiring;

    float lastFired;

    int maxShots = 3;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        shots = maxShots;
    }

    float shots;

    // Update is called once per frame
    void FixedUpdate()
    {
        Ray cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (groundPlane.Raycast(cursorRay, out float dist))
        {
            transform.LookAt(cursorRay.GetPoint(dist));
        }

        shots = Mathf.Clamp(shots + Time.deltaTime * RegenPerSecond, 0, maxShots);

        if (Input.GetMouseButton(0) && lastFired + fireDelay < Time.time && shots>=1)
        {
            shots -= 1;
            lastFired = Time.time;
            Rigidbody newProjectile = Instantiate<Rigidbody>(projectile, muzzle.position, default);
            newProjectile.velocity = muzzle.forward * 8;
        }

    }
}
