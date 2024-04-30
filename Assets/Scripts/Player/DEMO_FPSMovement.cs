using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum MovementState
{
    walking, sprinting, air
}
public class DEMO_FPSMovement : MonoBehaviour
{
    public Rigidbody rb;
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed = 7.0f;
    public float sprintSpeed = 10.0f;
    public float groundDrag = 5.0f;
    public float jumpForce = 12.0f;
    public float jumpCooldown = 0.25f;
    public float airMultiplier = 0.4f;

    public MovementState state;
    bool readyToJump;
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode spritKey = KeyCode.LeftShift;
    [Header("Ground Check")]
    public float playerHeigth = 2.0f;
    public LayerMask groundMask;
    [Header("Extra")]
    public Transform orientation;
    bool grounded;
    float horizonalInput;
    float verticalInput;
    Vector3 moveDirection;

    void Start()
    {
        rb.freezeRotation = true;
        readyToJump = true;
    }
    void FixedUpdate()
    {
        MovePlayer();
    }
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeigth * 0.5f + 0.2f, groundMask);

        SetupInput();
        SpeedControl();
        SateHandler();

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }
    void SetupInput()
    {
        horizonalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    void SateHandler()
    {
        if (grounded && Input.GetKey(spritKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else
        {
            state = MovementState.air;
        }
    }
    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizonalInput;
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }
    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}