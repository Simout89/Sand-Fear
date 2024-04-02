using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarFog : MonoBehaviour
{
    public static UnityEvent OnCarFog = new UnityEvent();


    [SerializeField] private GameObject Fog;

    private bool ActiveFog = true;

    private void Awake()
    {
        OnCarFog.AddListener(HandleCarFog);
    }

    private void HandleCarFog()
    {
        ActiveFog = !ActiveFog;
        Fog.SetActive(ActiveFog);
    }
}
