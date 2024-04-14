using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Zenject;

public class PlayerOxygen : MonoBehaviour
{
    [SerializeField] private VolumeProfile volume;
    [SerializeField] private BrokenPartSlot BrokenPartSlot;

    [SerializeField] private float MaxOxygen = 40;
    [SerializeField] private float Speed = 1f;

    [SerializeField] private float Oxygen;
    [SerializeField] private AudioSource audioSource;



    private PlayerLocation playerLocation;
    [Inject]
    public void Construct(PlayerLocation playerLocation)
    {
        this.playerLocation = playerLocation;
    }
    private void Awake()
    {
        Oxygen = MaxOxygen;
    }

    private void Update()
    {
        if(((playerLocation.GetLocation() == PlayerLocation.Locations.Car) && (BrokenPartSlot.Works)) || (playerLocation.GetLocation() == PlayerLocation.Locations.CutScene) || (playerLocation.GetLocation() == PlayerLocation.Locations.House))
        {
            if(Oxygen <= MaxOxygen)
            {
                Oxygen += Time.deltaTime * Speed * 2;
            }
        }else
        {
            if (Oxygen >= 0)
            {
                Oxygen -= Time.deltaTime * Speed;
            }
        }

        if (volume.TryGet(out Vignette vignette))
            vignette.intensity.Override(Mathf.Lerp(0.5f, 0, Oxygen / MaxOxygen));

        if (Oxygen < MaxOxygen / 2)
        {
            audioSource.mute = false;
        }else
        {
            audioSource.mute = true;
        }
    }

    private void OnApplicationQuit()
    {
        if (volume.TryGet(out Vignette vignette))
            vignette.intensity.Override(0);
    }
}
