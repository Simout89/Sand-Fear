using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class SoundController : MonoBehaviour
{
    public static UnityEvent onPlayerPosition = new UnityEvent();


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

    private void Awake()
    {
        onPlayerPosition.AddListener(HandlePlayerPosition);
    }

    private void HandlePlayerPosition()
    {
        CarEngine.mute = !CarEngine.mute;
        CarInEngine.mute = !CarInEngine.mute;
        CarSandStorm.mute = !CarSandStorm.mute;
        PlayerSandStorm.mute = !PlayerSandStorm.mute;
    }

    // Update is called once per frame
    void Update()
    {
        CarInEngine.pitch = Mathf.Clamp((Mathf.Abs(carController.leverHorizontal.Value) + Mathf.Abs(carController.leverVertical.Value) + 0.9f), 0.9f, 1.6f);
    }
}
