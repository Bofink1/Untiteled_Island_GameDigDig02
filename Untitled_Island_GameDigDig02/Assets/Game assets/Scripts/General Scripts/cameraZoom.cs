using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraZoom : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public float zoomSpeed = 2f;
    public float minZoom = 2f;
    public float maxZoom = 10f;

    Cinemachine3rdPersonFollow follow;

    void Start()
    {
        follow = vcam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        follow.CameraDistance -= scroll * zoomSpeed;
        follow.CameraDistance = Mathf.Clamp(follow.CameraDistance, minZoom, maxZoom);
    }
}
