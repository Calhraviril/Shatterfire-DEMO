using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private GameObject cam;
    [SerializeField] private Vector3 camPos;
    [SerializeField] private Vector3 aimPos;
    private Vector3 masterPos;


    private void Awake()
    {
        cam = GameObject.Find("Main Camera");
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            masterPos = aimPos;
        }
        else
        {
            masterPos = camPos;
        }

        cam.transform.position = transform.TransformPoint(masterPos);
        cam.transform.rotation = gameObject.transform.rotation;
    }
}
