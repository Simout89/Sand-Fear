using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour,IInteractive,ICollectable
{
    public bool Active { get; set; }
    [field: SerializeField] public Vector3 PlayerScale { get; set; } = Vector3.one;
    [field: SerializeField] public Vector3 PlayerPos { get; set; } = Vector3.zero;
    [field: SerializeField] public Vector3 ShelfScale { get; set ; } = Vector3.one;
    [field: SerializeField] public Vector3 ShelfPos { get; set; } = Vector3.zero;

    //[Header("Player")]
    //[SerializeField] private Vector3 playerScale = Vector3.one;
    //[SerializeField] private Vector3 playerPos = Vector3.zero;
    //[Header("Shelf")]
    //[SerializeField] private Vector3 shelfScale = Vector3.one;
    //[SerializeField] private Vector3 shelfPos = Vector3.zero;

    private Rigidbody rigidBody;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    public void Collect()
    {
        rigidBody.isKinematic = true;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    public void Put()
    {
        rigidBody.isKinematic = false;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    public void Activate()
    {

    }
}
