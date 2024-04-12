using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCameraController : MonoBehaviour
{
    private PlayerInput playerInput;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private Slider slider;
    private float mouseX;
    private float mouseY;

    private float xRotation;

    private bool HoldItem = false;

    private bool LockMouse = true;

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
        if (playerInput.EscButton)
            LockMouse = !LockMouse;

        if (LockMouse)
        {
            Time.timeScale = 1f;
            mouseX = playerInput.MouseX * slider.value * Time.deltaTime;
            mouseY = playerInput.MouseY * slider.value * Time.deltaTime;

            float minpos;
            if (HoldItem)
                minpos = 30f;
            else
                minpos = 90f;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, minpos);
            PlayerCamera.localRotation = Quaternion.Euler(xRotation, 0, 0);
            transform.Rotate(Vector3.up * mouseX);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }

    }
}


