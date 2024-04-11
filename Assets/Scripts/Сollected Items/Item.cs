using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour,IInteractive,ICollectable
{
    public bool Active { get; set; }
    public Vector3 PlayerScale { get; set; }
    public Vector3 PlayerPos { get; set; }
    public Vector3 ShelfScale { get; set ; }
    public Vector3 ShelfPos { get; set; }

    [Header("Player")]
    [SerializeField] private Vector3 playerScale = Vector3.one;
    [SerializeField] private Vector3 playerPos = Vector3.zero;
    [Header("Shelf")]
    [SerializeField] private Vector3 shelfScale = Vector3.one;
    [SerializeField] private Vector3 shelfPos = Vector3.zero;

    private Rigidbody rigidBody;
    private void Awake()
    {
        PlayerScale = playerScale;
        PlayerPos = playerPos;
        ShelfScale = shelfScale;
        ShelfPos = shelfPos;

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
}
