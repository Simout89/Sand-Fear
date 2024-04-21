using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CheckPointSystem : MonoBehaviour
{
    private PlayerLocation playerLocation;
    [Inject]
    public void Construct(PlayerLocation playerLocation)
    {
        this.playerLocation = playerLocation;
    }

    private GameObject Player;
    [Inject]
    public void Construct(GameObject Player)
    {
        this.Player = Player;
    }

    private Transform checkPoint;

    public void SetCheckPoint(Transform checkPoint)
    {
        this.checkPoint = checkPoint;
    }

    public void ReturnOnCheckPoint()
    {
        Player.GetComponent<CharacterController>().enabled = false;
        Player.GetComponent<CharacterController>().transform.position = checkPoint.transform.position;
        Player.GetComponent<CharacterController>().enabled = true;
        playerLocation.SetLocation(PlayerLocation.Locations.House);
    }
}
