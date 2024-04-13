using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
    public float HorizontalRaw { get; private set; }
    public float VerticalRaw { get; private set; }
    public bool InteractButtonItems { get; private set; }
    public float MouseX { get; private set; }
    public float MouseY { get; private set; }
    public bool Jump { get; private set; }
    public bool InteractButton { get; private set; }
    public bool SecondInteractButton { get; private set; }
    public bool InteractButtonHold { get; private set; }
    public bool EscButton { get; private set; }

    private void Update()
    {
        HorizontalRaw = Input.GetAxisRaw("Horizontal");
        VerticalRaw = Input.GetAxisRaw("Vertical");
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");
        Jump = Input.GetButtonDown("Jump");
        InteractButton = Input.GetButtonDown("Interact1");
        SecondInteractButton = Input.GetButtonDown("Interact2");
        EscButton = Input.GetButtonDown("Cancel");
        InteractButtonHold = Input.GetButton("Interact1");
        InteractButtonItems = Input.GetButtonDown("Interact3");
    }
}
