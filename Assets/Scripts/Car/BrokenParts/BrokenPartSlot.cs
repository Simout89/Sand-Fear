using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrokenPartSlot : MonoBehaviour, IInteractive, IShelf, IBrokenParts
{

    [SerializeField] private ItemValue.ItemType itemType;
    [SerializeField] private GameObject StartItem = null;
    [SerializeField] private float Speed = 1f;
    public bool Active { get; set; }
    public GameObject Item { get; set; }

    public float MaxValue { get; set; }
    public float Value { get; set; }
    public bool Works { get; set; } = false;

    [SerializeField] private GameObject[] ControlledObjects;
    [SerializeField] private Behaviour[] ControlledScripts;

    private void Awake()
    {
        if (StartItem != null)
        {
            Item = Instantiate(StartItem, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
            if (Item.TryGetComponent(out ICollectable iCollectable))
            {
                iCollectable.Collect();
            }
        }
    }

    private void Update()
    {
        if (Item != null && (Item.TryGetComponent(out ItemValue itemValue)) && (itemValue.type == itemType))
        {
            if (itemValue.Value > 0)
            {
                itemValue.Value -= Speed * Time.deltaTime;
                Value = itemValue.Value;
                MaxValue = itemValue.StartValue;
                Works = true;
            }
            else
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

        foreach (var obj in ControlledObjects)
        {
            obj.SetActive(Works);
        }
        foreach (var scr in ControlledScripts)
        {
            scr.enabled = Works;
        }
    }

    public void Activate()
    {
    }
}
