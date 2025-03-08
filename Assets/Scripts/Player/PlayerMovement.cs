using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    private Vector3 moveDirection;
    [SerializeField] private GameObject wallsAndPlane;
    static public PlayerMovement instance;
    private void Awake()
    {
       instance = this;
    }
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
      
    }

    void Update()
    {
        Walk();
        LookAround();
        wallsAndPlane.transform.position = new Vector3(wallsAndPlane.transform.position.x,wallsAndPlane.transform.position.y, transform.position.z) ;
    }
    private void FixedUpdate()
    {
        if (moveDirection != Vector3.zero)
        {
            rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
    private void Walk()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        moveDirection = moveDirection.normalized * PlayerStats.instance.walkSpeed;

    }
    private void LookAround()
    {
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X") * 5);
        }
      
    }
}
