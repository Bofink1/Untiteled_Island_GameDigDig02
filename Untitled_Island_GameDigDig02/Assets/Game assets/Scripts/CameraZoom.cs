using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public float zoomSpeed = 10f;
    public float minFOV = 20f;
    public float maxFOV = 60f;

    private void Update()
    {
        if (freeLookCamera == null) return;

        float scroll = Input.GetAxis("Mouse ScrollWheel"); 
        if (Mathf.Abs(scroll) > 0.01f)
        {
            freeLookCamera.m_Lens.FieldOfView -= scroll * zoomSpeed;
            freeLookCamera.m_Lens.FieldOfView = Mathf.Clamp(freeLookCamera.m_Lens.FieldOfView, minFOV, maxFOV);
            
        }
    }
}
