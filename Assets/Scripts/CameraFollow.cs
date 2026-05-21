using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float distance = 8f;
    public float smoothSpeed = 8f;
    public float mouseSensitivity = 3f;

    [Header("Vertical Limits")]
    public float minPitch = 10f;
    public float maxPitch = 70f;

    private float yaw = 0f;
    private float pitch = 20f;

    void Update()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        yaw += mouseDelta.x * mouseSensitivity * 0.1f;
        pitch -= mouseDelta.y * mouseSensitivity * 0.1f;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Remove "target.eulerAngles.y" to break the loop
        // Now the camera rotates purely based on mouse input
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        Vector3 desiredPosition = target.position + rotation * new Vector3(0f, 0f, -distance);

        // Using SmoothDamp or a high smoothSpeed keeps it responsive
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(target.position + Vector3.up * 0.5f);
    }

}