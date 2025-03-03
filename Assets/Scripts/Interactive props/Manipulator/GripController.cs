using UnityEngine;

public class GripController : MonoBehaviour
{
    [SerializeField] private AudioClip CloseClamp;
    private AudioSource _audioSource;
    [SerializeField] private Transform _center;
    [SerializeField] private Buttons _redButton;
    [SerializeField] private Lamp _lamp;
    private GameObject _holdObject = null;
    private bool _dropReady = false;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(_center.position, -transform.forward * 1);
    }
    private void Update()
    {
        RaycastHit hit;
        if ((Physics.Raycast(_center.position, -transform.forward, out hit, 1) && hit.collider.CompareTag("Stash")) || (_dropReady && _holdObject != null))
        {
            if((_dropReady && (_holdObject != null)) || (hit.collider.CompareTag("Stash") && (_holdObject == null)))
                _lamp.OnLamp();
            else
                _lamp.OffLamp();

            if (_redButton.Hold && (_holdObject == null) && hit.collider.TryGetComponent(out Stash stash))
            {
                _audioSource.PlayOneShot(CloseClamp);
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
