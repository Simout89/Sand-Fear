using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour,IInteractive,ICollectable
{
    public bool Active { get; set; }

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
}
