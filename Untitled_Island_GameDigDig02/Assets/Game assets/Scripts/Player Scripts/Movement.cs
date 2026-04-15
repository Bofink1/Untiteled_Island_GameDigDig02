using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    [Header("Movement Settings")]
    static public float walkspeed = 2f;
    static public float sprintspeed = 5f;
    public float acceleration = 20f;
    public float deceleration = 25f;

    [Header("Jump Settings")]
    public float jumpHeight = 2f;
    public float maxJumpHoldTime = 0.25f;
    private float jumpHoldCounter = 0f;

    [Header("Physics Settings")]
    public float gravity = -15f;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    public float currentStamina = 100f;
    public float jumpStaminaCost = 25f;
    public float staminaRegenRate = 15f;
    public float jumpStaminaThreshold = 25f;

    private Vector3 velocity;
    private bool isGrounded;

    public float turnSmoothTime = 0.3f;
    private float turnSmoothVelocity;

    public Transform groundCheck;
    private Animator animator;

    private Vector3 targetVelocity;
    private Vector3 currentVelocity;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            jumpHoldCounter = 0f;
        }

        // Input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 inputDir = new Vector3(horizontal, 0f, vertical).normalized;

        // Sprint detection
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintspeed : walkspeed;
        cam.GetComponent<Camera>().fieldOfView = Input.GetKey(KeyCode.LeftShift) ? 70 : 50;

        // Calculate target velocity
        if (inputDir.magnitude >= 0.1f)
        {
            // Compute camera-relative movement direction
            Vector3 moveDir = cam.TransformDirection(inputDir);
            moveDir.y = 0;
            moveDir.Normalize();

            if (moveDir.sqrMagnitude > 0.01f)
            {
                // Smoothly rotate toward actual movement direction
                float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
                float smoothAngle = Mathf.SmoothDampAngle(
                    transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
            }

            targetVelocity = moveDir * speed;
        }
        else
        {
            targetVelocity = Vector3.zero;
        }

        // Smoothceleration
        currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity,
            (targetVelocity.magnitude > currentVelocity.magnitude ? acceleration : deceleration) * Time.deltaTime);

        controller.Move(currentVelocity * Time.deltaTime);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded && currentStamina >= jumpStaminaThreshold)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpHoldCounter = maxJumpHoldTime;
            currentStamina -= jumpStaminaCost;
            Debug.Log(currentStamina);
        }

        // Stamina regen
        if (isGrounded && currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }

        // Hold jump for variable height
        if (Input.GetButton("Jump") && jumpHoldCounter > 0f)
        {
            velocity.y += -gravity * Time.deltaTime;
            jumpHoldCounter -= Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            jumpHoldCounter = 0f;
        }

        // Animations
        float speed2 = currentVelocity.magnitude;

        if (speed2 < 0.1f)
        {
            animator.SetFloat("speed", 0);
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetFloat("speed", 0.5f);
        }
        else
        {
            animator.SetFloat("speed", 1f);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
