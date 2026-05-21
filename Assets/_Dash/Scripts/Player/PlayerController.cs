using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float laneWidth = 3f;
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float laneChangeSpeed = 0.08f; // Tuned for fluid responsiveness
    [SerializeField] private float tiltAmount = 15f;
    [SerializeField] private float tiltSpeed = 8f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravity = -30f;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeedBoost = 25f;
    [SerializeField] private float dashDuration = 0.3f;
    [SerializeField] private float dashCooldown = 1.5f;
    [SerializeField] private UnityEngine.Rendering.Universal.DecalProjector dashDecal;
    [SerializeField] private AudioClip dashSound;

    private int currentLane = 0;
    private Vector3 targetPosition;
    private Vector3 currentVelocity;
    private float verticalVelocity;
    private bool isGrounded = true;
    private bool isDashing = false;
    private float dashTimeRemaining = 0f;
    private float nextDashTime = 0f;

    private Rigidbody rb;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction dashAction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        
        var col = GetComponent<BoxCollider>();
        col.isTrigger = true;

        targetPosition = transform.position;

        // Hook into Project-Wide Actions
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        dashAction = InputSystem.actions.FindAction("Sprint"); // Using Sprint action as Dash

        if (dashDecal != null)
        {
            dashDecal.gameObject.SetActive(false);
        }
        else
        {
            // Try to find the decal in scene if not assigned
            dashDecal = GameObject.Find("Decal Projector")?.GetComponent<UnityEngine.Rendering.Universal.DecalProjector>();
            if (dashDecal != null) dashDecal.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (GameManager.Instance == null || !GameManager.Instance.IsPlaying) return;

        if (isDashing)
        {
            dashTimeRemaining -= Time.deltaTime;
            if (dashTimeRemaining <= 0)
            {
                EndDash();
            }
        }

        HandleInput();
        ApplyMovement();
        ApplyRotation();
        
        // Increase speed over time
        if (!isDashing)
        {
            moveSpeed += 0.05f * Time.deltaTime;
        }
    }

    private void HandleInput()
    {
        if (moveAction.WasPressedThisFrame())
        {
            Vector2 moveInput = moveAction.ReadValue<Vector2>();
            if (moveInput.x < -0.1f && currentLane > -1)
            {
                currentLane--;
                UpdateTargetPosition();
            }
            else if (moveInput.x > 0.1f && currentLane < 1)
            {
                currentLane++;
                UpdateTargetPosition();
            }
        }

        if (jumpAction.WasPressedThisFrame() && isGrounded)
        {
            verticalVelocity = jumpForce;
            isGrounded = false;
        }

        if (dashAction.WasPressedThisFrame() && Time.time >= nextDashTime && !isDashing)
        {
            StartDash();
        }
    }

    private void StartDash()
    {
        isDashing = true;
        dashTimeRemaining = dashDuration;
        nextDashTime = Time.time + dashCooldown;

        if (AudioManager.Instance != null && dashSound != null)
        {
            AudioManager.Instance.PlaySFX(dashSound);
        }

        if (dashDecal != null)
        {
            dashDecal.gameObject.SetActive(true);
            // Parent it to player or keep it separate? Let's parent it for the duration
            dashDecal.transform.SetParent(transform);
            dashDecal.transform.localPosition = new Vector3(0, -0.9f, 0); // Position at feet
            dashDecal.transform.localRotation = Quaternion.Euler(90, 0, 0);
        }
    }

    private void EndDash()
    {
        isDashing = false;
        if (dashDecal != null)
        {
            dashDecal.gameObject.SetActive(false);
            dashDecal.transform.SetParent(null); // Detach so it doesn't follow death sequence if we want
        }
    }

    private void UpdateTargetPosition()
    {
        targetPosition = new Vector3(currentLane * laneWidth, transform.position.y, transform.position.z);
    }

    private void ApplyMovement()
    {
        // Forward movement
        float currentMoveSpeed = moveSpeed;
        if (isDashing)
        {
            currentMoveSpeed += dashSpeedBoost;
        }

        transform.Translate(Vector3.forward * currentMoveSpeed * Time.deltaTime, Space.World);

        // Vertical movement (Jump)
        if (!isGrounded)
        {
            verticalVelocity += gravity * Time.deltaTime;
            if (transform.position.y <= 0 && verticalVelocity < 0)
            {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                verticalVelocity = 0;
                isGrounded = true;
            }
        }

        // Horizontal movement (Lane switching)
        Vector3 currentPos = transform.position;
        float newX = Mathf.SmoothDamp(currentPos.x, currentLane * laneWidth, ref currentVelocity.x, laneChangeSpeed);
        float newY = currentPos.y + (verticalVelocity * Time.deltaTime);
        
        // Clamp Y to ground
        if (newY < 0 && isGrounded) newY = 0;

        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    private void ApplyRotation()
    {
        // Tilt based on horizontal movement
        float targetTilt = -currentVelocity.x * tiltAmount * 0.5f;
        Quaternion targetRot = Quaternion.Euler(0, 0, targetTilt);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * tiltSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (isDashing)
            {
                // Smash through obstacles!
                Destroy(other.gameObject);
                return;
            }
            
            OnDeath();
        }
    }

    private void OnDeath()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.EndGame();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
