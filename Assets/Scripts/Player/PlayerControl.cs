using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    // Muuttujat
    private CharacterController charon;

    [Header("Movement Values")]
    [SerializeField] private float forwardMoveSpeed;
    [SerializeField] private float backMoveSpeed;
    [SerializeField] private float alterMoveSpeed;

    [Header("Jump")]
    [SerializeField]private float jumpSTR = 0.4f;    

    private float jumpACCS = 0;
    private int jumpAmount = 0;
    float timer;

    private float horizontal; // Horizontal movement
    private float vertical; // Vertical movement
    private float rotEX; // The rotation given to the object


    void Awake()
    {
        // Player Control
        charon = GetComponent<CharacterController>();

        // Origin Rotation setter
        rotEX = transform.localRotation.eulerAngles.x;

        // Cursor Locking to game center
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        Rotator();

        InputControl();

        Move();
    }

    private void Rotator()
    {
        float cenX = GameObject.Find("Handler").GetComponent<GunControl>().senX;
        float mouseX = Input.GetAxis("Mouse X");
        rotEX += mouseX * cenX * Time.deltaTime;

        Quaternion localRotation = Quaternion.Euler(0.0f, rotEX, 0.0f);
        transform.rotation = localRotation;
    }

    private void InputControl()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void Move()
    {
        // Movement
        if (vertical != 0)
        {
            float moveSpeedUsed = (vertical > 0) ? forwardMoveSpeed : backMoveSpeed;
            charon.Move(transform.forward * moveSpeedUsed * Time.deltaTime * vertical);
        }
        if (horizontal != 0)
        {
            charon.Move(transform.right * alterMoveSpeed * Time.deltaTime * horizontal);
        }

        // Sneaking Code
        if (Input.GetKey(KeyCode.Q))
        {
            charon.height = 1;
        }
        else if (charon.height != 2)
        {
            charon.height = 2;
        }

        // Jumping Code
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpAmount < 2)
            {
                jumpACCS = jumpSTR;
                jumpAmount += 1;
            }
        }
        Jumper(); // Gravity and the reverse gravity called jumping
    }
    private void Jumper()
    {
        // Jump
        if (jumpACCS != 0 && timer < Time.time)
        {
            Vector3 jumping = new Vector3(0, jumpACCS, 0);
            charon.Move(jumping);
        }        
        if (charon.isGrounded)
        {
            jumpAmount = 0;
        }
        if (jumpACCS > -3.0)
        {
            jumpACCS = Mathf.MoveTowards(jumpACCS, -0.5f, Time.deltaTime * 2);
        }

    }
}