using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class CutSceneState : MonoBehaviour
{
    [SerializeField] private Behaviour[] scripts;

    public static UnityEvent<PlayerLocation.Locations> onCutScene = new UnityEvent<PlayerLocation.Locations>();
    private bool state = true;

    private PlayerLocation playerLocation;
    [Inject]
    public void Construct(PlayerLocation playerLocation)
    {
        this.playerLocation = playerLocation;
    }
    private void Awake()
    {
        onCutScene.AddListener(HandleCutScene);
    }

    private void HandleCutScene(PlayerLocation.Locations locations)
    {
        state = !state;
        for (int i = 0; i < scripts.Length; i++)
        {
            scripts[i].enabled = state;
        }
        playerLocation.SetLocation(locations);
    }
}
