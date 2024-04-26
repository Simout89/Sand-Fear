using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastAnim : MonoBehaviour
{
    [SerializeField] private Behaviour[] playerControllers;
    [SerializeField] private float delay = 1f;
    [SerializeField] private Transform parent; 
    [SerializeField] private Animator animator; 
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < playerControllers.Length; i++)
        {
            playerControllers[i].enabled = false;
            
        }
        other.gameObject.transform.parent = parent;
        other.gameObject.transform.localPosition = Vector3.zero;
        animator.SetTrigger("Eat");
        audioSource.PlayOneShot(audioSource.clip);
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(3);
    }
}
