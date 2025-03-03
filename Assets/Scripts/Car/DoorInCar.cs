using TMPro;
using UnityEngine;
using Zenject;

public class DoorInCar : MonoBehaviour, IInteractive, IHoldInteractive
{
    [SerializeField] private Transform PointToTeleport;
    [SerializeField] private AudioClip DoorSound;
    [SerializeField] private Location location;
    [SerializeField] private GameObject Indicator;
    [SerializeField] private Material onIndicator;
    [SerializeField] private Material offIndicator;

    [Header("RayCast")]
    [SerializeField] private float Distance = 5f;
    [SerializeField] private Transform StartPos;
    [SerializeField] private LayerMask layerMask;
    private AudioSource audioSource;
    public bool Active { get; set; }


    private GameObject Player;
    enum Location
    {
        inside,
        outside,
    }

    // ���������� ��������� ������������ ��� ��������� ������� � �������
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
    private PlayerLocation playerLocation;
    [Inject]
    public void Construct(PlayerLocation playerLocation)
    {
        this.playerLocation = playerLocation;
    }

    private void Update()
    {
        if (location == Location.inside)
        {
            if ((carController.leverHorizontal.Value == 0 && (carController.leverVertical.Value == 0)) && (!carController.DoorStuck))
            {
                Indicator.GetComponent<MeshRenderer>().material = onIndicator;
            }
            else
            {
                Indicator.GetComponent<MeshRenderer>().material = offIndicator;
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
            if(playerLocation.GetLocation() == PlayerLocation.Locations.World)
                playerLocation.SetLocation(PlayerLocation.Locations.Car);
            else
                playerLocation.SetLocation(PlayerLocation.Locations.World);
            Player.GetComponent<CharacterController>().enabled = false;
            Player.GetComponent<CharacterController>().transform.position = PointToTeleport.transform.position;
            Player.GetComponent<CharacterController>().enabled = true;
        }
    }

    public void Activate()
    {
    }
}
