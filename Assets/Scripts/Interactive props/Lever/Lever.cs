using System.Collections;
using UnityEngine;
using Zenject;

public class Lever : MonoBehaviour, IValueProps, IInteractive
{
    [Header("Settings")]
    [SerializeField] private float MaxValue = 1f;
    [SerializeField] private float MinValue = 0f;
    [SerializeField] private float DefaultValue = 0f;
    [SerializeField] private float Sensitivity = 0f;
    [SerializeField] private Orientation orientation = Orientation.Vertical;
    [Header("GameObject")]
    [SerializeField] private Transform Handle;
    public float Value { get; set; }
    public bool Active { get; set; } = false;

    private bool isDelay = true;
    private PlayerInteract playerInteract;
    [Inject]
    public void Construct(PlayerInteract playerInteract)
    {
        this.playerInteract = playerInteract;
    }

    private PlayerInput playerInput;
    [Inject]
    public void Construct(PlayerInput playerInput)
    {
        this.playerInput = playerInput;
    }

    private void Awake()
    {
        Value = DefaultValue;
        Handle.localRotation = Quaternion.Euler(0, 0, Mathf.Clamp(Value / MaxValue * 45, -45, 45));
    }

    // Update is called once per frame
    void Update()
    {
        if(Active && isDelay)
        {
            StartCoroutine(Delay());
            if(playerInteract.UseProps == false)
                Active = false;
        }
    }
    
    private IEnumerator Delay()
    {
        isDelay = false;
        if(orientation == Orientation.Vertical)
        {
            Value = Mathf.Clamp(Mathf.Round((Value + (playerInput.VerticalRaw * Time.deltaTime / 10)) * 10f) * 0.1f, MinValue, MaxValue);
        }else
            Value = Mathf.Clamp(Mathf.Round((Value + ((playerInput.HorizontalRaw * Time.deltaTime) / 10)) * 10f) * 0.1f, MinValue, MaxValue);
        Handle.localRotation = Quaternion.Euler(0, 0, Mathf.Clamp(Value/MaxValue * 45, -45, 45));
        yield return new WaitForSeconds(Sensitivity);
        isDelay = true;
    }

    public void Activate()
    {
    }

    enum Orientation
    {
        Horizontal,
        Vertical,
    }
}
