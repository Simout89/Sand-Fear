using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastAnim : MonoBehaviour
{
    [SerializeField] private Behaviour[] playerControllers;
    [SerializeField] private float delay = 1f;
    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < playerControllers.Length; i++)
        {
            playerControllers[i].enabled = false;
            
        }
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(3);
    }
}
