using System.Collections;
using UnityEngine;
using Zenject;

public class Note : MonoBehaviour, IInteractive, IValueProps
{
    [SerializeField] private Type type;
    [SerializeField] private Transform TeleportTarget;
    [SerializeField] private PlayerLocation.Locations Location;
    [SerializeField] private NoteScriptableObject noteScriptableObject;
    public bool Active { get; set; } = false;
    public float Value { get; set; }

    private bool FirstUse = true;

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
                uIController.OpenNote(noteScriptableObject.Text);
            else
            {
                if(FirstUse)
                {
                    StartCoroutine(Teleport());
                }
                FirstUse = false;
                uIController.CloseNote();
            }
        }else
        {
            if (Active)
                uIController.OpenNewspaper(noteScriptableObject.Text);
            else
            {
                uIController.CloseNewspaper();
            }
        }
    }
    
    private IEnumerator Teleport()
    {
        uIController.Blink();
        yield return new WaitForSeconds(0.5f);
        Player.GetComponent<CharacterController>().enabled = false;
        Player.GetComponent<CharacterController>().transform.position = TeleportTarget.transform.position;
        Player.GetComponent<CharacterController>().enabled = true;
        CutSceneState.onCutScene.Invoke(Location);
    }

    enum Type
    {
        Note,
        Newspaper
    }
}
