using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject Menu;

    private PlayerInput playerInput;
    [Inject]
    public void Construct(PlayerInput playerInput)
    {
        this.playerInput = playerInput;
    }

    private void Update()
    {
        if(playerInput.EscButton)
        {
            Menu.SetActive(!Menu.activeSelf);
        }
    }

    public void VolumeSlider()
    {
        AudioListener.volume = slider.value;
    }
}
