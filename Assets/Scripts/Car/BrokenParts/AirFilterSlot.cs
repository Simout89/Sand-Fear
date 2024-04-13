using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class AirFilterSlot : MonoBehaviour, IInteractive, IShelf, IBrokenParts
{
    [SerializeField] private GameObject StartItem = null;


    [SerializeField] private float Speed;
    public bool Active { get; set; }
    public GameObject Item { get; set; }

    public float MaxValue { get; set; }
    public float Value { get; set; }
    public bool Works { get; set; } = false;

    public void Activate()
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        if(StartItem != null)
        {
            Item = Instantiate(StartItem, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
            if(Item.TryGetComponent(out ICollectable iCollectable))
            {
                iCollectable.Collect();
            }
        }
    }

    private void Update()
    {
        if (Item != null && (Item.TryGetComponent(out ItemValue itemValue)) && (itemValue.type == ItemValue.ItemType.filter))
        {
            if (itemValue.Value > 0)
            {
                itemValue.Value -= Speed * Time.deltaTime;
                Value = itemValue.Value;
                MaxValue = itemValue.StartValue;
                Works = true;
            }else
            {
                MaxValue = itemValue.StartValue;
                Works = false;
                Value = 0;
            }
        }
        else
        {
            MaxValue = 1;
            Value = 0;
            Works = false;
        }
    }
}
