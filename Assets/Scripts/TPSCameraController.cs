using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;

public class TPSCameraController : MonoBehaviour
{
    [SerializeField] Transform cameraRoot;
    [SerializeField] Transform target;
    [SerializeField] float distance;
    [SerializeField] float mouseSesitivity;

    Vector2 inputDir;
    private float xRotation;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void FixedUpdate()
    {
        SetTargetPos();
    }

    private void LateUpdate()
    {
        xRotation -= inputDir.y * mouseSesitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        transform.Rotate(Vector3.up, inputDir.x * mouseSesitivity * Time.deltaTime);
        cameraRoot.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    private void SetTargetPos()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hitInfo, distance))
        {
            target.position = Camera.main.transform.position + Camera.main.transform.forward * (hitInfo.distance - 0.1f);
        }
        else
        {
            target.position = Camera.main.transform.position + Camera.main.transform.forward * distance;
        }
        target.rotation = Camera.main.transform.rotation;
    }

    private void OnLook(InputValue value)
    {
        inputDir = value.Get<Vector2>();
    }
}
