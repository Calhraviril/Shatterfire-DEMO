using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Vector3 camAddition = new Vector3 (1.5f, 1.5f, -2.5f);
    [SerializeField] private Vector3 playerPos;

    private void Awake()
    {

    }
    private void Update()
    {
        playerPos = GameObject.Find("Player").transform.position;
        gameObject.transform.position = playerPos + camAddition;
        print(gameObject.transform.position);
    }
}
