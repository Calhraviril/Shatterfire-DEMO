using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    public float rotX;
    public float rotY;

    public float senX;
    public float senY;
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotX = rot.x;
        rotY = rot.y;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotX += mouseX * senX * Time.deltaTime;
        rotY += mouseY * senY * Time.deltaTime;

        if (rotY < 80 || rotY > -80)
        {
            rotY = Mathf.Clamp(rotY, -80, 80);
        }
        Quaternion localRotation = Quaternion.Euler(rotY, rotX, 0.0f);
        transform.rotation = localRotation;
    }
}
