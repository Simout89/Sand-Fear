using UnityEngine;

public class Item : MonoBehaviour,IInteractive,ICollectable
{
    public bool Active { get; set; }
    [field: SerializeField] public Vector3 PlayerScale { get; set; } = Vector3.one;
    [field: SerializeField] public Vector3 PlayerPos { get; set; } = Vector3.zero;
    [field: SerializeField] public Vector3 PlayerRottation { get; set; } = Vector3.zero;
    [field: SerializeField] public Vector3 ShelfScale { get; set ; } = Vector3.one;
    [field: SerializeField] public Vector3 ShelfPos { get; set; } = Vector3.zero;

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
