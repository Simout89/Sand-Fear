using System.Collections;
using UnityEngine;

public class Points : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(Die());
    }
    private IEnumerator Die()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
