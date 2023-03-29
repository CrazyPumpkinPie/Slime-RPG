using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProjectileController : MonoBehaviour
{
    private Transform target;
    private Rigidbody projectileRb;
    private float projectileSpeed = 15;
    void Start()
    {
        projectileRb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        target = GameObject.FindGameObjectWithTag("Enemy").transform;
        Vector3 direction = (target.position - projectileRb.position + new Vector3(0, 1, 0)).normalized;
        projectileRb.AddForce(Vector3.up * 5,ForceMode.Impulse);
        projectileRb.velocity = (direction * projectileSpeed);
    }
}
