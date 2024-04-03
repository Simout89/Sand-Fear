using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

public class DoorInCar : MonoBehaviour, IInteractive, IHoldInteractive
{
    [SerializeField] private Transform PointToTeleport;
    [SerializeField] private AudioClip DoorSound;
    [SerializeField] private Location location;
    [SerializeField] private TMP_Text text;

    [Header("RayCast")]
    [SerializeField] private float Distance = 5f;
    [SerializeField] private float Radius = 1f;
    [SerializeField] private Transform StartPos;
    [SerializeField] private LayerMask layerMask;
    private AudioSource audioSource;
    public bool Active { get; set; }


    private GameObject Player;
    private bool DoorStuck = false;
    enum Location
    {
        inside,
        outside,
    }

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

    private void Update()
    {
        if (location == Location.inside)
        {
            if ((carController.leverHorizontal.Value == 0 && (carController.leverVertical.Value == 0)) && (!carController.DoorStuck))
            {
                text.text = "Open";
                text.color = Color.green;
            }
            else
            {
                text.text = "Close";
                text.color = Color.red;
            }
        }else
        {

            RaycastHit hit;
            if (Physics.Raycast(StartPos.position, -transform.right, out hit, Distance, ~layerMask))
            {
                carController.DoorStuck = true;
            }
            else
                carController.DoorStuck = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -transform.right * Distance);
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void ActiveHold()
    {
        if((carController.leverHorizontal.Value == 0 && (carController.leverVertical.Value == 0)) && (!carController.DoorStuck))
        {
            audioSource.PlayOneShot(DoorSound);
            PlayerLocation.onPlayerPosition.Invoke();
            Player.GetComponent<CharacterController>().enabled = false;
            Player.GetComponent<CharacterController>().transform.position = PointToTeleport.transform.position;
            Player.GetComponent<CharacterController>().enabled = true;
        }
    }
}
