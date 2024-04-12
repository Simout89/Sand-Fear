using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stash : MonoBehaviour
{
    [SerializeField] public GameObject Prefab;
    [SerializeField] private GameObject NextStesh;

    public void Next()
    {
        if(NextStesh != null)
            NextStesh.SetActive(true);
        gameObject.SetActive(false);
    }
}
