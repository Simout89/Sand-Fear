using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirFilterSlot : MonoBehaviour, IInteractive, IShelf
{
    [SerializeField] private float Speed;
    public bool Active { get; set; }
    public GameObject Item { get; set; }


    private void Update()
    {
        if(Item != null && (Item.TryGetComponent(out ItemValue itemValue)) && (itemValue.type == ItemValue.ItemType.filter))
        {
            if(itemValue.Value >= 0)
            {
                itemValue.Value -= Speed * Time.deltaTime;
            }
            
        }
    }
}
