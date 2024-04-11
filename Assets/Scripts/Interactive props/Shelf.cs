using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour,IInteractive,IShelf
{
    [SerializeField] private GameObject StartItem = null;
    public bool Active { get; set; }
    public GameObject Item { get; set; }

    public void Activate()
    {
    }

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
}
