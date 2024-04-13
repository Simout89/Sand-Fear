using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private Material offMaterial;
    [SerializeField] private Material onMaterial;
    [SerializeField] private Lever lever;
    [SerializeField] private Value value;


    private void Update()
    {
        if(value == Value.positive)
        { 
            if(lever.Value > 0)
            {
                gameObject.GetComponent<MeshRenderer>().material = onMaterial;
            }else
                gameObject.GetComponent<MeshRenderer>().material = offMaterial;
        }
        else
        {
            if (lever.Value < 0)
            {
                gameObject.GetComponent<MeshRenderer>().material = onMaterial;
            }
            else
                gameObject.GetComponent<MeshRenderer>().material = offMaterial;
        }
    }

    enum Value
    {
        positive,
        negative
    }
}
