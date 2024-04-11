using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class ItemValue : MonoBehaviour
{
    [SerializeField] public ItemType type;
    [SerializeField] public float StartValue = 1f;
    [ReadOnly] public float Value;

    private void Awake()
    {
        Value = StartValue;
    }

    public enum ItemType
    {
        filter,
        radarBoard
    }
}
