using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coordinates : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Transform Car;

    private void Update()
    {
        text.text = $"X:{Mathf.Round(Car.transform.position.x * 10) / 10}\nZ:{Mathf.Round(Car.transform.position.z * 10) / 10}";
    }
}
