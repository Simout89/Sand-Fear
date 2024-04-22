using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using static PlayerLocation;

public class CutSceneState : MonoBehaviour
{
    [SerializeField] private GameObject Monster;
    [SerializeField] private Behaviour[] scripts;
    [SerializeField] private Behaviour[] playerscripts;
    [SerializeField] private float CutScene2Lenght = 10;
    [SerializeField] private Transform NoteTarget;

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
    private GameObject Player;
    [Inject]
    public void Construct(GameObject Player)
    {
        this.Player = Player;
    }
    private UIController uIController;
    [Inject]
    public void Construct(UIController uIController)
    {
        this.uIController = uIController;
    }

    private void HandleCutScene(PlayerLocation.Locations locations)
    {
        state = !state;
        for (int i = 0; i < scripts.Length; i++)
        {
            scripts[i].enabled = state;
        }
        playerLocation.SetLocation(locations);


        if(locations == PlayerLocation.Locations.CutSceneTemple)
        {
            for (int i = 0; i < playerscripts.Length; i++)
            {
                playerscripts[i].enabled = false;
            }
            StartCoroutine(CutScene2());
        }
    }

    private IEnumerator CutScene2()
    {
        yield return new WaitForSeconds(CutScene2Lenght);
        StartCoroutine(Teleport());    
    }

    private IEnumerator Teleport()
    {
        yield return new WaitForSeconds(2f);
        uIController.Blink();
        yield return new WaitForSeconds(0.5f);
        Player.GetComponent<CharacterController>().enabled = false;
        Player.GetComponent<CharacterController>().transform.position = NoteTarget.transform.position;
        Player.GetComponent<CharacterController>().enabled = true;

        for (int i = 0; i < playerscripts.Length; i++)
        {
            playerscripts[i].enabled = true;
        }
        Monster.SetActive(true);
        onCutScene.Invoke(Locations.House);

    }
}
