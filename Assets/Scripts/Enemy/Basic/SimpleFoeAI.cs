using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFoeAI : MonoBehaviour
{
    private Transform selfPST;
    private Transform targetPST;
    private Rigidbody rigi;

    private bool grounded;

    [Header("Stats")]
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxFlight;
    [SerializeField] private int damage;

    private void Start()
    {
        selfPST = gameObject.transform;
        targetPST = gameObject.transform.Find("Targeted").transform;
        rigi = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 plaLoc = GameObject.Find("Player").transform.position;
        float step = speed * Time.deltaTime; 
        transform.LookAt(plaLoc);

        if (grounded)
        {
            if (rigi.velocity.x < maxSpeed || rigi.velocity.y < maxSpeed || rigi.velocity.z < maxSpeed)
            {
                Vector3 Wail = (targetPST.position - selfPST.position) * speed;
                rigi.AddForce(Wail, ForceMode.Impulse);
            }
            else
            {
                rigi.velocity = Vector3.zero;
            }
        }
    }

    private void OnCollisionEnter(Collision collision) // Damages the player on collision
    {
        PlayerMage magus = collision.gameObject.GetComponent<PlayerMage>();
        if (magus != null)
        {
            magus.PlayerDamager(damage);
            rigi.AddForce(Vector3.back * 10, ForceMode.Impulse);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        grounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }
}
