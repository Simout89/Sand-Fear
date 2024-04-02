using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerFog : MonoBehaviour
{
    public static UnityEvent OnPlayerFog = new UnityEvent();


    [SerializeField] private GameObject Fog;

    private bool ActiveFog = false;

    private void Awake()
    {
        OnPlayerFog.AddListener(HandlePlayerFog);
    }

    private void HandlePlayerFog()
    {
        ActiveFog = !ActiveFog;
        Fog.SetActive(ActiveFog);
    }
}
