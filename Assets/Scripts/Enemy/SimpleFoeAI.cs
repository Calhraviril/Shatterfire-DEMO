using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFoeAI : MonoBehaviour
{
    private Transform selfPST;
    private Transform targetPST;
    private Rigidbody rigi;
    
    [Header("Movement values")]
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;

    private void Start()
    {
        selfPST = gameObject.transform;
        targetPST = GameObject.Find("Targeted").transform;
        rigi = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
        //Vector3 walke = new Vector3(selfPST.position.x, 0, selfPST.position.z);
        //Vector3 walky = new Vector3(targetPST.position.x, 0, targetPST.position.z);
        gameObject.transform.LookAt(GameObject.Find("Player").transform);

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
