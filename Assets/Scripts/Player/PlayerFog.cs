using UnityEngine;
using Zenject;

public class PlayerFog : MonoBehaviour
{
    [SerializeField] public GameObject Fog;
    [SerializeField] public Material FogMaterial;

    private PlayerLocation playerLocation;
    [Inject]
    public void Construct(PlayerLocation playerLocation)
    {
        this.playerLocation = playerLocation;
    }
    private void Update()
    {
        if(playerLocation.GetLocation() == PlayerLocation.Locations.World)
        {
            Fog.SetActive(true);
        }else
            Fog.SetActive(false);
    }
}
