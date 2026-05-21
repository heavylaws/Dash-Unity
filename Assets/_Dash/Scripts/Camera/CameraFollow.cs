using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 4, -8);
    public float smoothSpeed = 0.125f;

    [Header("Juice Settings")]
    public float rollAmount = 1.5f;
    public float rollSpeed = 5f;
    public float baseFOV = 65f;
    public float maxFOV = 80f;
    public float fovGrowth = 0.5f;

    private Camera cam;
    private float currentRoll;
    private float currentFOV;

    void Start()
    {
        cam = GetComponent<Camera>();
        currentFOV = baseFOV;
        if (cam != null) cam.fieldOfView = currentFOV;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Smooth Position Follow
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Roll based on target position (relative to center)
        float targetRoll = -target.position.x * rollAmount;
        currentRoll = Mathf.Lerp(currentRoll, targetRoll, Time.deltaTime * rollSpeed);

        // Look at target with a slight offset upwards
        transform.LookAt(target.position + Vector3.up * 1.5f);
        transform.Rotate(0, 0, currentRoll);

        // Stable FOV System
        if (cam == null) cam = GetComponent<Camera>();

        if (cam != null)
        {
            bool isPlaying = GameManager.Instance != null && GameManager.Instance.IsPlaying;
            bool isReady = GameManager.Instance == null || GameManager.Instance.IsReady;

            if (isPlaying)
            {
                // Smoothly move towards maxFOV during gameplay
                currentFOV = Mathf.MoveTowards(currentFOV, maxFOV, Time.deltaTime * fovGrowth);
            }
            else if (isReady)
            {
                // Reset/Maintain FOV at base in Ready state
                currentFOV = baseFOV;
            }
            // If Paused or GameOver, currentFOV simply doesn't update (stays at its current value)

            cam.fieldOfView = currentFOV;
        }
    }
}
