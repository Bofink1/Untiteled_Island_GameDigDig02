using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    [Header("Movement Settings")]
    static public float walkspeed = 3f;
    static public float sprintspeed = 6f;
    public float acceleration = 20f;  // how quickly you accelerate
    public float deceleration = 25f;  // how quickly you decelerate

    [Header("Jump Settings")]
    public float jumpHeight = 2f;
    public float maxJumpHoldTime = 0.25f; // max time jump can be held
    private float jumpHoldCounter = 0f;

    [Header("Physics Settings")]
    public float gravity = -15f;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("Stamina Settings")]
    public float maxStamina = 100f; //maximum stamina
    public float currentStamina = 100f;
    public float jumpStaminaCost = 25f; //how much it costs to jump
    public float staminaRegenRate = 15f; // per second
    public float jumpStaminaThreshold = 25f; // minimum needed to jump

    private Vector3 velocity;
    private bool isGrounded;

    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public Transform groundCheck;

    // Target movement direction & current velocity for smoothing
    private Vector3 targetVelocity;
    private Vector3 currentVelocity;

    void Update()
    {
        // Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            jumpHoldCounter = 0f; // reset jump hold timer
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
            float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            targetVelocity = moveDir * speed;
        }
        else
        {
            targetVelocity = Vector3.zero;
        }

        // Smoothceleration
        currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, (targetVelocity.magnitude > currentVelocity.magnitude ? acceleration : deceleration) * Time.deltaTime);

        controller.Move(currentVelocity * Time.deltaTime);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded && currentStamina >= jumpStaminaThreshold)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpHoldCounter = maxJumpHoldTime;
            currentStamina -= jumpStaminaCost;
            Debug.Log(currentStamina);
        }

        //Stamina cost for jumping
        if (isGrounded && currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
            
        }

        // Hold jump for variable height
        if (Input.GetButton("Jump") && jumpHoldCounter > 0f)
        {
            velocity.y += -gravity * Time.deltaTime; // keep adding upward force
            jumpHoldCounter -= Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            jumpHoldCounter = 0f; // stops jump boost
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
   

}
