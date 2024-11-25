using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // VARIABLES
    [SerializeField] private float xSensitivity;
    [SerializeField] private float ySensitivity;

    public Transform orientation;

    public float xRotation;
    public float yRotation = 180f;

    public GameObject textCanvas;

    //public bool flipOrientation;

    // REFERENCES

    private void Start()
    {
        CursorSettings();

        //if (flipOrientation)
        //{
        //    yRotation = 0f;
        //}
    }

    private void Update()
    {
        if (textCanvas.GetComponent<Canvas>().enabled != true) // only move the camera when the canvas isn't open
        {
            GetMouseInput();
            RotateCamera();
            RotateOrientation();
        }
    }

    private void CursorSettings()
    {
        // lock & hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void GetMouseInput()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * xSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * ySensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }

    private void RotateCamera() // come back and make this smoother
    {
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    private void RotateOrientation() // come back and make this smoother
    {
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
