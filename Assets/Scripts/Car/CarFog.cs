using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class CarFog : MonoBehaviour
{
    [SerializeField] private GameObject Fog;
    private PlayerLocation playerLocation;
    [Inject]
    public void Construct(PlayerLocation playerLocation)
    {
        this.playerLocation = playerLocation;
    }

    private void Update()
    {
        if(playerLocation.Location == PlayerLocation.Locations.Car)
        {
            Fog.SetActive(true);
        }else
            Fog.SetActive(false);

    }
}
