using UnityEngine;
using Zenject;

public class Pleer : MonoBehaviour, IUseableItem
{

    private bool Playing = false;
    private AudioSource audioSource;

    public bool Active { get; set; }

    public void Use()
    {
        Playing = !Playing;
        SoundPlay();
    }

    private void Awake() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void SoundPlay()
    {
        if (Playing)
        {
            audioSource.Play();
        }else
            audioSource.Stop();
    }

}
