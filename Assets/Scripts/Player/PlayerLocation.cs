using UnityEngine;
using UnityEngine.Events;

public class PlayerLocation : MonoBehaviour
{

    private Locations Location = Locations.Car; // true = car, false = world

    public enum Locations
    {
        World,
        Car,
        House,
        CutScene
    }

    public void SetLocation( Locations loc )
    {
        Location = loc;
    }

    public Locations GetLocation()
    {
        return Location;
    }
}
