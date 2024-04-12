using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class GripController : MonoBehaviour
{
    [SerializeField] private Transform Center;
    [SerializeField] private Transform CenterItem;
    [SerializeField] private Buttons RedButton;
    [SerializeField] private Lamp lamp;
    private GameObject HoldObject = null;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(Center.position, -transform.forward * 1);
    }
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Center.position, -transform.forward, out hit, 1) && ((hit.collider.tag == "Stash") || (hit.collider.tag == "DropPlace")))
        {
            lamp.OnLamp();
            if (RedButton.Hold && hit.collider.TryGetComponent(out Stash stash) && (HoldObject == null) && (hit.collider.tag == "Stash"))
            {
                HoldObject = Instantiate(stash.Prefab, CenterItem.transform.position, Quaternion.Euler(-90f,0,0), gameObject.transform);
                stash.Next();
            }
            if(HoldObject != null && (hit.collider.tag == "DropPlace") && RedButton.Hold)
            {
                HoldObject.transform.parent = null;
                if(HoldObject.TryGetComponent(out Rigidbody rigidbody))
                    rigidbody.isKinematic = false;
                HoldObject = null;
            }
        }else
        {
            lamp.OffLamp();
        }
    }
}
