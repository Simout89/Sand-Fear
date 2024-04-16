using System.Collections;
using UnityEngine;
using Zenject;
using static PlayerLocation;
using static UnityEditor.FilePathAttribute;

public class StashTrigger : MonoBehaviour
{
    [SerializeField] private Transform TeleportTarget;
    private PlayerLocation playerLocation;
    [Inject]
    public void Construct(PlayerLocation playerLocation)
    {
        this.playerLocation = playerLocation;
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

    [SerializeField] private ManipulatorRadio _manipulatorRadio;
    public enum TypeItem
    {
        knife,
        statue
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out ItemInStash itemInStash) && (itemInStash.GetFirstUse() == true))
        {
            itemInStash.Use();
            _manipulatorRadio.PlayWithItemType(itemInStash.GetTypeItem());
            if(itemInStash.GetTypeItem() == TypeItem.statue)
            {
                StartCoroutine(Teleport());
            }
        }
    }
    private IEnumerator Teleport()
    {
        yield return new WaitForSeconds(2f);
        uIController.Blink();
        yield return new WaitForSeconds(0.5f);
        playerLocation.SetLocation(Locations.World);
        Player.GetComponent<CharacterController>().enabled = false;
        Player.GetComponent<CharacterController>().transform.position = TeleportTarget.transform.position;
        Player.GetComponent<CharacterController>().enabled = true;
    }
}

