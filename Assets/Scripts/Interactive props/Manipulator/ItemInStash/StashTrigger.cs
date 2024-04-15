using UnityEngine;
using Zenject;
using static PlayerLocation;

public class StashTrigger : MonoBehaviour
{
    private PlayerLocation playerLocation;
    [Inject]
    public void Construct(PlayerLocation playerLocation)
    {
        this.playerLocation = playerLocation;
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
                playerLocation.SetLocation(Locations.World);
            }
        }
    }
}
