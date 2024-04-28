using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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

    private UIController UIController;
    [Inject]
    public void Construct(UIController UIController)
    {
        this.UIController = UIController;
    }

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
        if (playerInput.EscButton && (UIController.Directory.activeSelf == false) && (UIController.NewspaperObject.activeSelf == false) && (UIController.NoteObject.activeSelf == false))
            LockMouse = !LockMouse;

        if (LockMouse)
        {
            Time.timeScale = 1f;
            mouseX = playerInput.MouseX * slider.value * Time.deltaTime;
            mouseY = playerInput.MouseY * slider.value * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
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


