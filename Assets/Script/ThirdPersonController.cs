using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public float walkSpeed = 6.0f;
    public float runSpeed = 12.0f;
    public float jumpForce = 8.0f;
    public float gravity = 9.81f;
    public float rotationSpeed = 10.0f;
    
    public Transform cameraTransform; // Reference to the camera
    
    private Vector3 velocity;
    private CharacterController controller;
    private Animator animator;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        
        // Convert input direction to camera-relative direction
        Vector3 moveDirection = Vector3.zero;
        if (cameraTransform != null)
        {
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;
            forward.y = 0; // Ignore vertical component
            right.y = 0;
            forward.Normalize();
            right.Normalize();
            
            moveDirection = (forward * vertical + right * horizontal).normalized;
        }

        if (moveDirection.magnitude >= 0.1f)
        {
            // Smooth rotation towards movement direction
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        Vector3 move = moveDirection * currentSpeed;
        if (isGrounded)
        {
            velocity.y = -0.5f; // Keep character grounded
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = jumpForce;
            }
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime; // Apply gravity
        }

        controller.Move((move + velocity) * Time.deltaTime);

        // Update animator (if available)
        if (animator != null)
        {
            animator.SetFloat("Speed", moveDirection.magnitude * (isRunning ? 2.0f : 1.0f), 0.1f, Time.deltaTime);
            animator.SetBool("isJumping", !isGrounded);
        }
    }
}