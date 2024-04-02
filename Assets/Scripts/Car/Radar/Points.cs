using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(Die());
    }
    private IEnumerator Die()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
