using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CarController : MonoBehaviour
{

    private CharacterController characterController;

    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float speed = 12f;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private float rotateScale = 10f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] public Lever leverVertical;
    [SerializeField] public Lever leverHorizontal;

    public bool DoorStuck = false;

    private Vector3 velocity;
    private bool isGrounded;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 move = transform.forward * leverVertical.Value;

        characterController.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

        transform.Rotate(Vector3.up * (leverHorizontal.Value / rotateScale) );
    }
}
