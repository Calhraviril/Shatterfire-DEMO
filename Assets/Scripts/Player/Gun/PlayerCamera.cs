using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private GameObject locker;
    [SerializeField] private Vector3 camPos;
    [SerializeField] private Vector3 aimPos;
    private Vector3 masterPos;




    private void Awake()
    {
        locker = GameObject.Find("Handler");
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            masterPos = aimPos;
            GameObject.Find("PlayerChar").GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            masterPos = camPos;
            GameObject.Find("PlayerChar").GetComponent<MeshRenderer>().enabled = true;
        }

        gameObject.transform.position = locker.transform.TransformPoint(masterPos);
        gameObject.transform.rotation = locker.transform.rotation;
    }
}
