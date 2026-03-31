using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraNew : MonoBehaviour
{
    public Transform player;         // Player pivot (chest/head)
    public float mouseSensitivity = 3f;
    public float distance = 5f;      // Camera distance behind player
    public float height = 2f;        // Camera height from player
    public float minPitch = -40f;    // How far you can look down
    public float maxPitch = 80f;     // How far you can look up
    public float rotationSpeed = 120f; // Degrees per second for A/D turning

    private float yaw = 0f;
    private float pitch = 20f;

    void LateUpdate()
    {
        // Right-click to rotate camera
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            yaw += mouseX;
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }

        // Player input
        float vertical = Input.GetAxis("Vertical");   // W/S
        float horizontal = Input.GetAxis("Horizontal"); // A/D

        // Rotate player horizontally using A/D
        if (Mathf.Abs(horizontal) > 0.01f)
        {
            player.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime);
        }

        // Rotate player to face camera when moving forward/back
        if (Mathf.Abs(vertical) > 0.01f)
        {
            Vector3 forward = Quaternion.Euler(0, yaw, 0) * Vector3.forward;
            forward.y = 0;
            player.forward = forward;
        }

        // Camera position behind player
        Vector3 offset = Quaternion.Euler(pitch, yaw, 0) * new Vector3(0, 0, -distance);
        transform.position = player.position + Vector3.up * height + offset;

        // Camera looks at player
        transform.LookAt(player.position + Vector3.up * height);
    }
}
