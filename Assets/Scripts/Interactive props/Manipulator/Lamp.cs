using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField] private Material offMaterial;
    [SerializeField] private Material onMaterial;


    public void OnLamp()
    {
        gameObject.GetComponent<MeshRenderer>().material = onMaterial;
    }

    public void OffLamp()
    {
        gameObject.GetComponent<MeshRenderer>().material = offMaterial;
    }
}
