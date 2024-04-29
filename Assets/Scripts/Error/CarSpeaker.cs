using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class CarSpeaker : MonoBehaviour
{
    [SerializeField] private Buttons button;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        button.OnClick += HandleClick;
    }
    private void OnDisable()
    {
        button.OnClick -= HandleClick;
    }
    private void HandleClick()
    {
        audioSource.Stop();
    }
}
