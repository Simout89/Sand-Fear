using UnityEngine;
using UnityEngine.Events;

public class PlayerLocation : MonoBehaviour
{

    public static UnityEvent<Locations> onPlayerPosition = new UnityEvent<Locations>(); // оепедекюрэ

    public Locations Location = Locations.Car; // true = car, false = world

    private void Awake()
    {
        onPlayerPosition.AddListener(HandlePlayerPosition);
    }

    private void HandlePlayerPosition(Locations locations)
    {
        Location = locations;
    }

    public enum Locations
    {
        World,
        Car,
        House,
        CutScene
    }
}
