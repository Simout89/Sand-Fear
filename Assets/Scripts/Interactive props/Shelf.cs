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
            Item = Instantiate(StartItem, gameObject.transform.position, Quaternion.Euler(0, -180f, 0), gameObject.transform);
            if (Item.TryGetComponent(out ICollectable iCollectable))
            {
                Item.transform.localPosition = iCollectable.ShelfPos;
                iCollectable.Collect();

            }
        }
    }
}
