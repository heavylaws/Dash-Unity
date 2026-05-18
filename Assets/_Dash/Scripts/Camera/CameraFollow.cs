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
    public float maxFOV = 90f;
    public float fovGrowth = 0.1f;

    private Camera cam;
    private float currentRoll;
    private float currentFOV;

    void Start()
    {
        cam = GetComponent<Camera>();
        currentFOV = baseFOV;
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

        // Simple FOV Juice
        if (cam != null)
        {
            currentFOV = Mathf.Min(maxFOV, currentFOV + Time.deltaTime * fovGrowth);
            cam.fieldOfView = currentFOV;
        }
    }
}
