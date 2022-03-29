using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float rotEX;
    private float speed;
    private float alterspeed;
    void Start()
    {
        rotEX = transform.localRotation.eulerAngles.x;
    }
    void Update()
    {
        Rotator();
        Speeder();
        Alterspeeder();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= 2;
            alterspeed *= 2;
        }
    }
    private void FixedUpdate()
    {
        transform.Translate(alterspeed * Time.deltaTime, 0, speed * Time.deltaTime, Space.Self);
    }

    private void Speeder()
    {
        if (Input.GetKey(KeyCode.W))
        {
            speed = 10;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            speed = -10;
        }
        else
        {
            speed = 0;
        }
    }
    private void Alterspeeder()
    {
        if (Input.GetKey(KeyCode.D))
        {
            alterspeed = 10;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            alterspeed = -10;
        }
        else
        {
            alterspeed = 0;
        }
    }
    private void Rotator()
    {
        float mouseX = Input.GetAxis("Mouse X");
        rotEX += mouseX * 100 * Time.deltaTime;

        Quaternion localRotation = Quaternion.Euler(0.0f, rotEX, 0.0f);
        transform.rotation = localRotation;
    }
}
