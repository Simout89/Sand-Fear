using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInCar : MonoBehaviour, IInteractive, IHoldInteractive
{
    [SerializeField] private Transform PointToTeleport;
    public bool Active { get; set; }

    private GameObject Player;
    private void Awake()
    {
        Player = GameObject.Find("Player");
    }
    // Update is called once per frame
    private void Update()
    {
        
    }

    public void ActiveHold()
    {
        Player.GetComponent<CharacterController>().enabled = false;
        Player.GetComponent<CharacterController>().transform.position = PointToTeleport.transform.position;
        Player.GetComponent<CharacterController>().enabled = true;
    }
}
