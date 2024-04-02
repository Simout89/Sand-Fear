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
    private CarController carController;
    [Inject]
    public void Construct(CarController carController)
    {
        this.carController = carController;
    }
    // Update is called once per frame

    public void ActiveHold()
    {
        if(carController.leverHorizontal.Value == 0 && (carController.leverVertical.Value == 0))
        {
            PlayerFog.OnPlayerFog.Invoke();
            CarFog.OnCarFog.Invoke();
            SoundController.onPlayerPosition.Invoke();
            Player.GetComponent<CharacterController>().enabled = false;
            Player.GetComponent<CharacterController>().transform.position = PointToTeleport.transform.position;
            Player.GetComponent<CharacterController>().enabled = true;
        }
    }
}
