using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;

    private CharacterController characterController;

    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float speed = 12f;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private AudioSource WalkSound;

    private Vector3 velocity;
    private bool isGrounded;

    private PlayerLocation playerLocation;
    [Inject]
    public void Construct(PlayerLocation playerLocation)
    {
        this.playerLocation = playerLocation;
    }
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if(playerLocation.Location == PlayerLocation.Locations.World)
        {
            if (Mathf.Abs(playerInput.Horizontal) + Mathf.Abs(playerInput.Vertical) <= 0.9)
            {
                WalkSound.mute = true;
            }
            else
            {
                WalkSound.mute = false;
            }
        }else
            WalkSound.mute = true;

        Vector3 move = transform.right * playerInput.Horizontal + transform.forward * playerInput.Vertical;

        characterController.Move(move * speed * Time.deltaTime);
        if (playerInput.Jump && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }
}
