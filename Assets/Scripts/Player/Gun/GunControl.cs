using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    private float rotX;
    private float rotY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Vector3 rot = transform.localRotation.eulerAngles;
        rotX = rot.x;
        rotY = rot.y;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotX += mouseX * 100 * Time.deltaTime;
        rotY += mouseY * 100 * Time.deltaTime;

        if (rotY < 80 || rotY > -80)
        {
            rotY = Mathf.Clamp(rotY, -80, 80);
        }

        Quaternion localRotation = Quaternion.Euler(rotY, rotX, 0.0f);
        transform.rotation = localRotation;

    }
}
