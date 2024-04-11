using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStateMachine : MonoBehaviour
{
    [SerializeField] private AudioClip Introduction;

    private State state;


    private void Awake()
    {
        state = State.Awake;
    }



    enum State
    {
        Awake
    }
}
