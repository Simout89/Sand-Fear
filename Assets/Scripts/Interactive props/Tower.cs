using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Tower : MonoBehaviour
{
    [SerializeField] private Buttons button;
    private Animator animator;
    private AudioSource audioSource;

    private bool fisrtUse = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        button.OnClick += HandleClick;
    }

    private void OnDisable()
    {
        button.OnClick -= HandleClick;
    }

    private void HandleClick()
    {
        if(fisrtUse)
        {
            Debug.Log("On");
            audioSource.PlayOneShot(audioSource.clip);
            fisrtUse = false;
            animator.SetTrigger("Light");
        }
    }
}
