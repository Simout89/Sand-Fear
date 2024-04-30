using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Compas : MonoBehaviour
{
    [SerializeField] private Transform RotatablePart;
    [SerializeField] private Transform CarBody;
    private Vector3 CompasCenter;

    private CarController carController;
    [Inject]
    public void Construct(CarController carController)
    {
        this.carController = carController;
    }
    private void Awake()
    {
        CompasRotation();
    }
    private void OnEnable()
    {
        carController.CarRotateChange += HandleCarRotateChange;
    }
    private void OnDisable()
    {
        carController.CarRotateChange -= HandleCarRotateChange;
    }
    private void HandleCarRotateChange()
    {
        CompasRotation();
    }

    private void CompasRotation()
    {
        Quaternion rotation = CarBody.transform.rotation;
        rotation.z = 0;
        rotation.x = 0;
        RotatablePart.transform.localRotation = rotation;
    }
}
