using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource CarInEngine;
    [SerializeField] private AudioSource CarSandStorm;
    [SerializeField] private AudioSource CarEngine;
    [SerializeField] private AudioSource PlayerSandStorm;
    private CarController carController;



    [Inject]
    public void Construct(CarController carController)
    {
        this.carController = carController;
    }

    private PlayerLocation playerLocation;
    [Inject]
    public void Construct(PlayerLocation playerLocation)
    {
        this.playerLocation = playerLocation;
    }

    // Update is called once per frame
    void Update()
    {
        CarInEngine.mute = !playerLocation.Location;
        CarSandStorm.mute = !playerLocation.Location;

        PlayerSandStorm.mute = playerLocation.Location;
        CarEngine.mute = playerLocation.Location;

        CarInEngine.pitch = Mathf.Clamp((Mathf.Abs(carController.leverHorizontal.Value) + Mathf.Abs(carController.leverVertical.Value) + 0.9f), 0.9f, 1.6f);
    }
}
