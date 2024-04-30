using System;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public event Action CarRotateChange;


    private Rigidbody _rigidBody;

    [SerializeField] private float speed = 12f;
    [SerializeField] private float rotateScale = 10f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] public Lever leverVertical;
    [SerializeField] public Lever leverHorizontal;

    public bool DoorStuck = false;

    private bool isGrounded = false;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        Vector3 move = transform.forward * leverVertical.Value;

        _rigidBody.MovePosition(_rigidBody.position + (move * speed * Time.deltaTime));

        transform.Rotate(Vector3.up * (leverHorizontal.Value * rotateScale * Time.deltaTime));

        if (leverHorizontal.Value != 0f)
        {
            CarRotateChange?.Invoke();
        }
    }
}
