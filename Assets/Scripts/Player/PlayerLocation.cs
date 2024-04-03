using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerLocation : MonoBehaviour
{
    public static UnityEvent onPlayerPosition = new UnityEvent();

    public bool Location { get; private set; } = true;

    private void Awake()
    {
        onPlayerPosition.AddListener(HandlePlayerPosition);
    }

    private void HandlePlayerPosition()
    {
        Location = !Location;
    }
}
