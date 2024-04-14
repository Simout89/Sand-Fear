using UnityEngine;
using Zenject;

public class Buttons : MonoBehaviour,IInteractive,IButton
{
    [SerializeField] private Transform ButtonBody;
    public bool Active { get; set; }
    public bool Hold { get; set; }

    private Vector3 startPos;

    private Vector3 endPos;

    private PlayerInteract playerInteract;
    [Inject]
    public void Construct(PlayerInteract playerInteract)
    {
        this.playerInteract = playerInteract;
    }

    public void Activate()
    {
    }


    private void Awake()
    {
        startPos = ButtonBody.transform.localPosition;
        Quaternion localRotation = ButtonBody.transform.localRotation;
        Vector3 localOffset = new Vector3(0, 0f, -0.01f);
        Vector3 globalOffset = localRotation * localOffset;
        endPos = startPos + globalOffset;
        
    }
    void Update()
    {
        if(playerInteract.UseProps && (playerInteract.RayTarget == gameObject))
        {
            ButtonBody.transform.localPosition = endPos;
            Hold = true;
        }
        else
        {
            ButtonBody.transform.localPosition = startPos;
            Hold = false;
        }
    }
}
