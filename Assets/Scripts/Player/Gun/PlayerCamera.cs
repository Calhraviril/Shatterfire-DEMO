using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Vector3 camPos;
    [SerializeField] private Vector3 aimPos;
    [SerializeField] private Vector3 sneakPos;

    private GameObject locker;
    private Vector3 masterPos;

    private void Awake()
    {
        locker = GameObject.Find("Handler");
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            masterPos = aimPos;
            GameObject.Find("PlayerChar").GetComponent<MeshRenderer>().enabled = false;
        }
        else if (GameObject.Find("Player").GetComponent<CharacterController>().height != 2)
        {
            masterPos = sneakPos;
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
