using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MonsterWalkSound : MonoBehaviour
{
    [SerializeField] private AudioClip Clip;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            audioSource.PlayOneShot(Clip);
            Debug.Log(321);
        }
    }
}
