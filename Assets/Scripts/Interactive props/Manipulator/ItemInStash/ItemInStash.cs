using UnityEngine;

public class ItemInStash : MonoBehaviour
{
    [SerializeField] private StashTrigger.TypeItem TypeItem;
    private bool FirstUse = true;

    public void Use()
    {
        if(FirstUse == true)
            FirstUse = false;
    }

    public bool GetFirstUse()
    {
        return FirstUse;
    }

    public StashTrigger.TypeItem GetTypeItem()
    {
        return TypeItem;
    }
}
