using UnityEngine;

public class GripController : MonoBehaviour
{
    [SerializeField] private Transform _center;
    [SerializeField] private Buttons _redButton;
    [SerializeField] private Lamp _lamp;
    private GameObject _holdObject = null;
    private bool _dropReady = false;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(_center.position, -transform.forward * 1);
    }
    private void Update()
    {
        RaycastHit hit;
        if ((Physics.Raycast(_center.position, -transform.forward, out hit, 1) && hit.collider.tag == "Stash") || _dropReady)
        {
            if((_dropReady && _holdObject) || (hit.collider.tag == "Stash" && !_holdObject))
            {
                _lamp.OnLamp();
            }else
                _lamp.OffLamp();
            if (_redButton.Hold && (_holdObject == null) && hit.collider.TryGetComponent(out Stash stash))
            {
                _holdObject = Instantiate(stash.Prefab, _center.transform.position, Quaternion.Euler(-90f, 0, 0), gameObject.transform);
                stash.Next();
            }
            if (_holdObject != null && _dropReady && _redButton.Hold)
            {
                if (_holdObject.TryGetComponent(out Rigidbody rigidbody))
                    rigidbody.isKinematic = false;
                _holdObject.transform.parent = null;
                _holdObject = null;
            }
        }
        else
        {
            _lamp.OffLamp();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DropPlace")
        {
            _dropReady = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "DropPlace")
        {
            _dropReady = false;
        }
    }
}
