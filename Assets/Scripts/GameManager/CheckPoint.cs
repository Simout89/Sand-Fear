using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CheckPoint : MonoBehaviour
{
    private CheckPointSystem checkPointSystem;
    [Inject]
    public void Construct(CheckPointSystem checkPointSystem)
    {
        this.checkPointSystem = checkPointSystem;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            checkPointSystem.SetCheckPoint(transform);
        }
    }
}
