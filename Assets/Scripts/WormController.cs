using UnityEngine;
using UnityEngine.InputSystem;

public class WormController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float acceleration = 10f;
    public float deceleration = 8f;

    [Header("Jump")]
    public float minJumpForce = 3f;
    public float maxJumpForce = 10f;
    public float jumpHoldTime = 0.4f;
    public float gravityScale = 2.5f;
    private bool isJumping = false;
    private float jumpTimer = 0f;
    private bool wasGrounded = false;
    private bool jumpPressed = false;

    [Header("Ground Check")]
    public float groundCheckDistance = 0.3f;
    public LayerMask groundLayer;

    [Header("Abilities")]
    public bool hasDoubleJump = false;
    private bool usedDoubleJump = false;

    [Header("Dash")]
    public bool hasDash = false;
    public float dashForce = 15f;
    public float dashDuration = 0.15f;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private bool usedDash = false;

    public bool isGrounded = false;
    private Rigidbody rb;
    private Vector3 currentVelocity;

    // Input System
    public InputSystem_Actions inputActions;
    public Vector2 moveInput;
    private bool jumpHeld = false;
    public bool jumpReleased = false;

    [Header("Ground Check")]
    public Transform feetPoint; // Drag your new 'FeetPoint' object here in the Inspector
    public float groundCheckRadius = 0.2f; // The size of the detection bubble

    void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    void OnEnable()
    {
        inputActions.Player.Enable();

        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        inputActions.Player.Sprint.performed += ctx =>
        {
            HandleDash();
        };

        inputActions.Player.Jump.performed += ctx => jumpPressed = true;
        inputActions.Player.Jump.canceled += ctx =>
        {
            jumpHeld = false;
            jumpReleased = true;
        };
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        jumpHeld = inputActions.Player.Jump.IsPressed();
        CheckGround();
        HandleJump();
        HandleDashTimer();
    }

    void FixedUpdate()
    {
        HandleMovement();
        ApplyGravity();
    }
    void HandleMovement()
    {
        if (isDashing) return;

        Transform cam = Camera.main.transform;
        Vector3 camForward = Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized;
        Vector3 camRight = Vector3.ProjectOnPlane(cam.right, Vector3.up).normalized;

        Vector3 moveDirection = (camForward * moveInput.y + camRight * moveInput.x * 0.1f).normalized;
        Vector3 targetVelocity = moveDirection * moveSpeed;

        float rate = moveInput.magnitude > 0 ? acceleration : deceleration;
        currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, rate * Time.fixedDeltaTime);

        // Worm always faces camera forward direction
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(camForward), 15f * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector3(currentVelocity.x, rb.linearVelocity.y, currentVelocity.z);
    }

    void HandleJump()
    {
        // Only reset when we've just landed (transition from air to ground)
        if (isGrounded && !wasGrounded)
        {
            usedDoubleJump = false;
            isJumping = false;
            jumpTimer = 0f;
        }
        wasGrounded = isGrounded;

        // Initial jump on press
        if (jumpPressed && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, minJumpForce, rb.linearVelocity.z);
            isJumping = true;
            jumpTimer = 0f;
        }

        // Hold to go higher
        if (jumpHeld && isJumping && jumpTimer < jumpHoldTime)
        {
            jumpTimer += Time.deltaTime;
            float extraForce = Mathf.Lerp(0f, maxJumpForce - minJumpForce, jumpTimer / jumpHoldTime);
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, minJumpForce + extraForce, rb.linearVelocity.z);
        }
        else if (!jumpHeld && isJumping)
        {
            jumpTimer = jumpHoldTime;
        }

        // Double jump
        if (jumpReleased && !isGrounded && isJumping && hasDoubleJump && !usedDoubleJump)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, minJumpForce * 1.5f, rb.linearVelocity.z);
            usedDoubleJump = true;
            jumpTimer = jumpHoldTime;
        }

        jumpPressed = false;
        jumpReleased = false;
    }

    void HandleDash()
    {
        if (hasDash && !isGrounded && !usedDash)
        {
            isDashing = true;
            dashTimer = dashDuration;
            usedDash = true;

            Vector3 dashDirection = transform.forward;
            rb.linearVelocity = new Vector3(dashDirection.x * dashForce, 0f, dashDirection.z * dashForce);
        }
    }

    void HandleDashTimer()
    {
        if (isGrounded) usedDash = false;

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                isDashing = false;
            }
        }
    }

    void ApplyGravity()
    {
        // Suppress gravity completely during dash
        if (isDashing) return;
        rb.AddForce(Vector3.down * gravityScale, ForceMode.Acceleration);
    }

    void CheckGround()
    {
        if (feetPoint == null) return;

        // Checks a tiny sphere bubble at the player's feet. 
        // It doesn't use rays, so it can't get cut off by wall/floor seams!
        isGrounded = Physics.CheckSphere(feetPoint.position, groundCheckRadius, groundLayer);
    }


    public void UnlockAbility(AbilityType ability)
    {
        if (ability == AbilityType.DoubleJump)
            hasDoubleJump = true;
        else if (ability == AbilityType.Dash)
            hasDash = true;
    }

}