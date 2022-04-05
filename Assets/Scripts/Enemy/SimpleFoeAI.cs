using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFoeAI : MonoBehaviour
{
    private Transform selfPST;
    private Transform targetPST;
    
    [Header("Movement values")]
    [SerializeField] private float speed;

    private void Start()
    {
        selfPST = gameObject.transform;
        targetPST = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(selfPST.position, targetPST.position, step);
    }
}
