using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GripTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float Distance = 1;
    public bool Trigger = false;

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit, Distance, ~layerMask))
        {
            Trigger = true;
        }else
            Trigger = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.up * Distance);
    }
}

