using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //declaring variables
    [SerializeField] Transform PlayerTransform;
    Transform CameraTransform;

    float MouseMin = -20.0f;
    float MouseMax = 40.0f;
    Vector3 CameraOffset;
    Vector3 CameraLookat;
    float CameraLookHeight = 1.5f;
    float mouseX;
    float mouseY;
    void Start()
    {
        //initializing variables
        CameraTransform = transform;
        CameraOffset = new Vector3(0, 2.0f, -2.0f);
    }

    void Update()
    {
        //takes mouse input to move camera within its maximum range
        mouseX += Input.GetAxis("Mouse X");
        mouseY -= Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, MouseMin, MouseMax);
    }

    void LateUpdate()
    {
        Quaternion camRotation = Quaternion.Euler(mouseY, mouseX, 0);
        CameraTransform.position = PlayerTransform.position + camRotation * CameraOffset;
        CameraLookat = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y + CameraLookHeight, PlayerTransform.position.z);
        CameraTransform.LookAt(CameraLookat);
    }
}