using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    // Muuttujat
    private CharacterController charon;

    [Header("Movement Values")]
    [SerializeField] private float forwardMoveSpeed = 7.5f;
    [SerializeField] private float backMoveSpeed = 3f;
    [SerializeField] private float alterMoveSpeed = 7.5f;
    [SerializeField] private float gravCount;

    //[Header("Jump")]
    //[SerializeField]
    //private float jumpSTR = 0.4f;
    //private float jumpACCS = 0;
    //private int jumpAmount = 0;

    private float horizontal;
    private float vertical;
    private float rotEX;

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
        float mouseX = Input.GetAxis("Mouse X");
        rotEX += mouseX * 100 * Time.deltaTime;

        Quaternion localRotation = Quaternion.Euler(0.0f, rotEX, 0.0f);
        transform.rotation = localRotation;
    }

    private void InputControl()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (jumpAmount < 2)
        //    {
        //        jumpACCS = jumpSTR;
        //        jumpAmount += 1;
        //    }
        //}
    }

    private void Move()
    {
        if (vertical != 0)
        {
            float moveSpeedUsed = (vertical > 0) ? forwardMoveSpeed : backMoveSpeed;
            charon.Move(transform.forward * moveSpeedUsed * Time.deltaTime * vertical);
        }
        if (horizontal != 0)
        {
            charon.Move(transform.right * alterMoveSpeed * Time.deltaTime * horizontal);
        }
        if (!charon.isGrounded)
        {
            charon.Move(-transform.up * Time.deltaTime * gravCount);
        }
    }
}
