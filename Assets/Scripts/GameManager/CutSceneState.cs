using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class CutSceneState : MonoBehaviour
{
    [SerializeField] private Behaviour[] scripts;
    public static UnityEvent onCutScene = new UnityEvent();
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

    private void HandleCutScene()
    {
        state = !state;
        for (int i = 0; i < scripts.Length; i++)
        {
            scripts[i].enabled = state;
        }
        playerLocation.SetLocation(PlayerLocation.Locations.CutScene);
    }
}
