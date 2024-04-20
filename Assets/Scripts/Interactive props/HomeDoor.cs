using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HomeDoor : MonoBehaviour, IInteractive, IHoldInteractive
{
    [SerializeField] private Transform TeleportTarget;
    private GameObject Player;
    private CharacterController playerCharacterController;
    [Inject]
    public void Construct(GameObject Player)
    {
        this.Player = Player;
    }
    private PlayerLocation playerLocation;
    [Inject]
    public void Construct(PlayerLocation playerLocation)
    {
        this.playerLocation = playerLocation;
    }
    public bool Active { get; set; }

    public void Activate()
    {
    }

    private void Awake()
    {
        playerCharacterController = Player.GetComponent<CharacterController>();
    }

    public void ActiveHold()
    {
        if(playerLocation.GetLocation() == PlayerLocation.Locations.World)
        {
            playerLocation.SetLocation(PlayerLocation.Locations.House);
        }else
        {
            playerLocation.SetLocation(PlayerLocation.Locations.World);
        }
        playerCharacterController.enabled = false;
        playerCharacterController.transform.position = TeleportTarget.transform.position;
        playerCharacterController.enabled = true;
    }
}
