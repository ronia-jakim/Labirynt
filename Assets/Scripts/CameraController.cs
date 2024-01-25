using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    private Transform playerBody;
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerBody = transform.parent;
    }

    void Update()
    {
        CameraRotation();
    }

    private void CameraRotation () {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        xRotation -= mouseY * mouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 80f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX * mouseSensitivity * Time.deltaTime);
    }
}
