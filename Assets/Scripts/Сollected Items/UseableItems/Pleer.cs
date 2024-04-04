using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Pleer : MonoBehaviour
{

    private bool Playing = false;
    private AudioSource audioSource;
    private PlayerInput playerInput;
    private Item item;

    [Inject]
    public void Construct(PlayerInput playerInput)
    {
        this.playerInput = playerInput;
    }
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        item = GetComponent<Item>();
    }
    private void Update()
    {
        if(playerInput.InteractButtonItems && item.Active)
        {
            Playing = !Playing;
            SoundPlay();
        }
    }

    private void SoundPlay()
    {
        if (Playing)
        {
            audioSource.Play();
        }else
            audioSource.Stop();
    }

}
