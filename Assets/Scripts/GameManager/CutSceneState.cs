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
    [SerializeField] private float CutScene3Lenght = 10;
    [SerializeField] private Transform NoteTarget;
    [SerializeField] private Transform NoteTarget2;
    [SerializeField] private GameObject LastTrigger;

    [SerializeField] private int teleportcount = 0;

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
        if(teleportcount == 0)
        {
            yield return new WaitForSeconds(CutScene2Lenght);
        }else
            yield return new WaitForSeconds(CutScene3Lenght);
        StartCoroutine(Teleport());    
    }

    private IEnumerator Teleport()
    {
        yield return new WaitForSeconds(2f);
        uIController.Blink();
        yield return new WaitForSeconds(0.5f);
        Player.GetComponent<CharacterController>().enabled = false;
        if(teleportcount == 0)
        {
            Player.GetComponent<CharacterController>().transform.position = NoteTarget.transform.position;
        }else
        {
            Player.GetComponent<CharacterController>().transform.position = NoteTarget2.transform.position;
            LastTrigger.SetActive(true);
        }
        Player.GetComponent<CharacterController>().enabled = true;


        teleportcount++;
        for (int i = 0; i < playerscripts.Length; i++)
        {
            playerscripts[i].enabled = true;
        }
        Monster.SetActive(true);
        onCutScene.Invoke(Locations.House);

    }
}
