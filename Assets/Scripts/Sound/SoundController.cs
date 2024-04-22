using UnityEngine;
using Zenject;

public class SoundController : MonoBehaviour
{
    [SerializeField] private GameObject InCarSound;
    [SerializeField] private GameObject OutCarSound;
    [SerializeField] private GameObject IndCutScene;
    [SerializeField] private GameObject House;
    [SerializeField] private GameObject TempleCutScene;


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
                    IndCutScene.SetActive(false);
                    OutCarSound.SetActive(false);
                    TempleCutScene.SetActive(false);
                    House.SetActive(false);
                    InCarSound.SetActive(true);
                };break;
            case PlayerLocation.Locations.World:
                {
                    TempleCutScene.SetActive(false);
                    House.SetActive(false);
                    IndCutScene.SetActive(false);
                    OutCarSound.SetActive(true);
                    InCarSound.SetActive(false);
                };break;
            case PlayerLocation.Locations.CutSceneInd:
                {
                    TempleCutScene.SetActive(false);
                    House.SetActive(false);
                    IndCutScene.SetActive(true);
                    OutCarSound.SetActive(false);
                    InCarSound.SetActive(false);
                }; break;
            case PlayerLocation.Locations.House:
                {
                    TempleCutScene.SetActive(false);
                    House.SetActive(true);
                    IndCutScene.SetActive(false);
                    OutCarSound.SetActive(false);
                    InCarSound.SetActive(false);
                }; break;
            case PlayerLocation.Locations.CutSceneTemple:
                {
                    TempleCutScene.SetActive(true);
                    House.SetActive(false);
                    IndCutScene.SetActive(false);
                    OutCarSound.SetActive(false);
                    InCarSound.SetActive(false);
                }; break;
        }

        

        CarInEngine.pitch = Mathf.Lerp(0.9f,1.6f, (Mathf.Abs(carController.leverHorizontal.Value) + Mathf.Abs(carController.leverVertical.Value)));
    }
}
