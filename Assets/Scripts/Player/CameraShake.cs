using UnityEngine;
using Zenject;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private float Speed = 14f;
    [SerializeField] private float Amount = 0.05f;
    private PlayerInput playerInput;
    [Inject]
    public void Construct(PlayerInput playerInput)
    {
        this.playerInput = playerInput;
    }

    private float timer = 0;
    private float defaultPosY = 0;

    private void Awake()
    {
        defaultPosY = cam.transform.localPosition.y;
    }

    private void Update()
    {
        if(Mathf.Abs(playerInput.VerticalRaw) + Mathf.Abs(playerInput.HorizontalRaw) > 0)
        {
            timer += Time.deltaTime * Speed;
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * Amount, cam.transform.localPosition.z);
        }
        else
        {
            timer = 0;
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, Mathf.Lerp(cam.transform.localPosition.y, defaultPosY, Time.deltaTime * Speed), cam.transform.localPosition.z);
        }
    }
}
