using UnityEngine;

/// <summary>
/// Handles player movement and basic interaction.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float airControlMultiplier = 0.0f;
    public float jumpForce = 8f;
    public float gravity = -20f;

    [Header("Jump Settings")]
    public int maxJumps = 2;
    public float doubleJumpForceMultiplier = 0.75f;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private int jumpsRemaining;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        jumpsRemaining = maxJumps;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            jumpsRemaining = maxJumps;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        float controlFactor = isGrounded ? 1f : airControlMultiplier;
        controller.Move(move * moveSpeed * controlFactor * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && jumpsRemaining > 0)
        {
            float force = jumpForce;
            if (jumpsRemaining < maxJumps) force *= doubleJumpForceMultiplier;
            velocity.y = Mathf.Sqrt(force * -2f * gravity);
            jumpsRemaining--;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public float GetCurrentSpeed() => controller.velocity.magnitude;
    public int GetJumpsRemaining() => jumpsRemaining;
}