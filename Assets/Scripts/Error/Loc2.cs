using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Loc2 : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;


    private void OnTriggerEnter(Collider other)
    {
        playableDirector.Play();
    }

    private void OnTriggerExit(Collider other)
    {

    }
}
