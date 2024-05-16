using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("移動設定")]
    public float moveSpeed;
    public float jumpForce;          
    public float jumpCooldown;       
    public float groundDrag;         
    public float airMultiplier;      

    [Header("按鍵綁定")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("基本設定")]
    public Transform PlayerCamera;   

    [Header("地板確認")]
    public float playerHeight;       
    public LayerMask whatIsGround;   
    public bool grounded;            

    private bool readyToJump;        
    private float horizontalInput;   
    private float verticalInput;     

    private Vector3 moveDirection;   

    private Rigidbody rbFirstPerson; 

    private void Start()
    {
        rbFirstPerson = GetComponent<Rigidbody>();
        rbFirstPerson.freezeRotation = true;         
        readyToJump = true;
    }

    private void Update()
    {
        MyInput();
        SpeedControl();   

    
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
        Debug.DrawRay(transform.position, new Vector3(0, -(playerHeight * 0.5f + 0.3f), 0), Color.red); 
        if (grounded)
            rbFirstPerson.drag = groundDrag;
        else
            rbFirstPerson.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();          
    }

    
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown); 
        }
    }

    private void MovePlayer()
    {
       
        moveDirection = PlayerCamera.forward * verticalInput + PlayerCamera.right * horizontalInput;

        
        if (grounded)
        {
            moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
            rbFirstPerson.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        
        else if (!grounded)
            rbFirstPerson.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

   
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rbFirstPerson.velocity.x, 0f, rbFirstPerson.velocity.z); 

       
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rbFirstPerson.velocity = new Vector3(limitedVel.x, rbFirstPerson.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        
        rbFirstPerson.velocity = new Vector3(rbFirstPerson.velocity.x, 0f, rbFirstPerson.velocity.z);
      
        rbFirstPerson.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}