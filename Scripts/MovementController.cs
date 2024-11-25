using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float groundDrag;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    public Transform orientation;
    bool readyToJump;

    [Header("Ground Check")]
    [SerializeField] private float playerHeight = 1; // figure out how to set this automatically
    public LayerMask Ground;
    bool grounded;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    [Header("Camera Controller")]
    public GameObject cameraController;

    float horizontalInput;
    float verticalInput;

    Vector3 movementDirection;

    Rigidbody rb;

    private void Start()
    {
        readyToJump = true;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        MyInput();
        GroundCheck();  
        HandleDrag();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        movementDirection = (orientation.forward * verticalInput) + (orientation.right * horizontalInput);
        // on ground
        if (grounded)
            rb.AddForce(movementDirection.normalized * movementSpeed * 10f, ForceMode.Force);
        // in air
        else if (!grounded)
            rb.AddForce(movementDirection.normalized * movementSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void RotatePlayer()
    {
        //this.transform.rotation = orientation.transform.rotation;
        this.transform.rotation = Quaternion.Euler(0, cameraController.GetComponent<CameraController>().yRotation, 0);
    }

    private void GroundCheck()
    {
        grounded = Physics.Raycast(transform.position + Vector3.up, Vector3.down, playerHeight + 0.2f, Ground);
    }

    private void HandleDrag()
    {
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVelocity.magnitude > movementSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * movementSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // add force
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
