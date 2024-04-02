using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    private PlayerInput playerInput;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private float mouseSensivity = 100f;
    private float mouseX;
    private float mouseY;

    private float xRotation = 0f;

    private bool HoldItem = false;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        Cursor.lockState = CursorLockMode.Locked;
        PlayerInteract.OnHoldItem.AddListener(HandleHoldItem);
    }

    private void HandleHoldItem()
    {
        HoldItem = !HoldItem;
    }

    private void Update()
    {
        mouseX = playerInput.MouseX * mouseSensivity * Time.deltaTime;
        mouseY = playerInput.MouseY * mouseSensivity * Time.deltaTime;

        float minpos;
        if (HoldItem)
            minpos = 30f;
        else
            minpos = 90f;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, minpos);
        PlayerCamera.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }
}
