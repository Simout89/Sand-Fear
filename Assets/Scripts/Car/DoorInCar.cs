using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

public class DoorInCar : MonoBehaviour, IInteractive, IHoldInteractive
{
    [SerializeField] private Transform PointToTeleport;
    public bool Active { get; set; }

    private GameObject Player;

    // Используем внедрение зависимостей для получения доступа к объекту
    [Inject]
    public void Construct(GameObject Player)
    {
        this.Player = Player;
    }
    // Update is called once per frame

    public void ActiveHold()
    {
        Player.GetComponent<CharacterController>().enabled = false;
        Player.GetComponent<CharacterController>().transform.position = PointToTeleport.transform.position;
        Player.GetComponent<CharacterController>().enabled = true;
    }
}
