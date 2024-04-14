using UnityEngine;
using UnityEngine.Events;

public class CutSceneState : MonoBehaviour
{
    [SerializeField] private Behaviour[] scripts;
    public static UnityEvent onCutScene = new UnityEvent();
    private bool state = true;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        onCutScene.AddListener(HandleCutScene);
    }

    private void HandleCutScene()
    {
        state = !state;
        for (int i = 0; i < scripts.Length; i++)
        {
            scripts[i].enabled = state;
        }
        PlayerLocation.onPlayerPosition.Invoke(PlayerLocation.Locations.CutScene);
    }
}
