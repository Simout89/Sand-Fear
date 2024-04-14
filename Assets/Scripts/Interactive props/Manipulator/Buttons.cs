using UnityEngine;
using Zenject;

public class Buttons : MonoBehaviour,IButton
{
    [SerializeField] private Transform ButtonBody;
    public bool Hold { get; set; }

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
        ButtonBody.transform.localPosition = endPos;
        Hold = true;
    }

    public void Relize()
    {
        ButtonBody.transform.localPosition = startPos;
        Hold = false;
    }
}
