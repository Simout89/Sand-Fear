using UnityEngine;
using Zenject;

public class Note : MonoBehaviour, IInteractive, IValueProps
{
    [SerializeField] private Type type;
    [SerializeField] private Transform TeleportTarget;
    [SerializeField] private string text;
    public bool Active { get; set; } = false;
    public float Value { get; set; }

    private UIController uIController;
    [Inject]
    public void Construct(UIController uIController)
    {
        this.uIController = uIController;
    }
    private GameObject Player;
    [Inject]
    public void Construct(GameObject Player)
    {
        this.Player = Player;
    }
    public void Activate()
    {
        if(type == Type.Note)
        {
            if (Active)
                uIController.OpenNote(text);
            else
            {
                Player.GetComponent<CharacterController>().enabled = false;
                Player.GetComponent<CharacterController>().transform.position = TeleportTarget.transform.position;
                Player.GetComponent<CharacterController>().enabled = true;
                CutSceneState.onCutScene.Invoke();
                uIController.CloseNote();
            }
        }else
        {
            if (Active)
                uIController.OpenNewspaper(text);
            else
            {
                uIController.CloseNewspaper();
            }
        }
    }
    
    enum Type
    {
        Note,
        Newspaper
    }
}
