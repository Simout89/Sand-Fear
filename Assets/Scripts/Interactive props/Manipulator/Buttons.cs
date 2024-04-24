using System;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class Buttons : MonoBehaviour,IButton,IInteractive
{
    [SerializeField] private Transform ButtonBody;

    public event Action OnClick;

    public bool Hold { get; set; }
    public bool Active { get; set; }

    private Vector3 startPos;

    private Vector3 endPos;


    private void Awake()
    {
        startPos = ButtonBody.transform.localPosition;
        Quaternion localRotation = ButtonBody.transform.localRotation;
        Vector3 localOffset = new Vector3(0, 0f, -0.01f);
        Vector3 globalOffset = localRotation * localOffset;
        endPos = startPos + globalOffset;
        
    }

    public void Press()
    {
        OnClick?.Invoke();
        ButtonBody.transform.localPosition = endPos;
        Hold = true;
    }

    public void Relize()
    {
        ButtonBody.transform.localPosition = startPos;
        Hold = false;
    }

    public void Activate() { }
}
