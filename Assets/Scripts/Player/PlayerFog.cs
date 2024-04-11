using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PlayerFog : MonoBehaviour
{
    [SerializeField] public GameObject Fog;
    [SerializeField] public Material FogMaterial;

    private PlayerLocation playerLocation;
    [Inject]
    public void Construct(PlayerLocation playerLocation)
    {
        this.playerLocation = playerLocation;
    }
    private void Update()
    {
        Fog.SetActive(!playerLocation.Location);
    }
}
