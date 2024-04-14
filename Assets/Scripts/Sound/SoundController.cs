using UnityEngine;
using Zenject;

public class SoundController : MonoBehaviour
{
    [SerializeField] private GameObject InCarSound;
    [SerializeField] private GameObject OutCarSound;


    [SerializeField] private AudioSource CarInEngine;

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

    // Update is called once per frame
    void Update()
    {
        switch (playerLocation.GetLocation())
        {
            case PlayerLocation.Locations.Car:
                {
                    OutCarSound.SetActive(false);
                    InCarSound.SetActive(true);
                };break;
            case PlayerLocation.Locations.World:
                {
                    OutCarSound.SetActive(true);
                    InCarSound.SetActive(false);
                };break;
        }

        

        CarInEngine.pitch = Mathf.Lerp(0.9f,1.6f, (Mathf.Abs(carController.leverHorizontal.Value) + Mathf.Abs(carController.leverVertical.Value)));
    }
}
