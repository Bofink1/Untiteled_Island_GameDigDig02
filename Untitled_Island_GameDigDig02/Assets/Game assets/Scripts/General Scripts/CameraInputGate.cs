using Cinemachine;
using UnityEngine;

public class FreeLookInputGate : MonoBehaviour
{
    [Header("Sensitivity")]
    public float xSensitivity = 2f;
    public float ySensitivity = 1.5f;

    private CinemachineFreeLook _freeLook;

    private void Awake()
    {
        _freeLook = GetComponent<CinemachineFreeLook>();

        _freeLook.m_XAxis.m_InputAxisName = "";
        _freeLook.m_YAxis.m_InputAxisName = "";
    }

    private void Update()
    {
        if (Input.GetMouseButton(1)) // right click held
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _freeLook.m_XAxis.m_InputAxisValue = Input.GetAxis("Mouse X") * xSensitivity;
            _freeLook.m_YAxis.m_InputAxisValue = -Input.GetAxis("Mouse Y") * ySensitivity;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            _freeLook.m_XAxis.m_InputAxisValue = 0f;
            _freeLook.m_YAxis.m_InputAxisValue = 0f;
        }
    }
}
