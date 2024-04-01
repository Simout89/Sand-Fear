using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float Distance = 1f;
    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject SliderGameObject;
    [SerializeField] private Slider Slider;
    private bool DelaySlider = true;
    private bool Delay = true;

    [SerializeField] private PlayerMovement MoveScript;
    [SerializeField] private PlayerCameraController CameraScript;

    private PlayerInput playerInput;
    public bool UseProps { get; private set; } = false;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Distance) && hit.collider.TryGetComponent(out IInteractive iInteractive))
        {
            indicator.SetActive(true);
            if(playerInput.InteractButton && hit.collider.TryGetComponent(out IValueProps iValueProps))
            {
                iInteractive.Active = !iInteractive.Active;
                UseProps = !UseProps;
                LockMove();
            } // toggle кнопки

            if (hit.collider.TryGetComponent(out IHoldInteractive iHoldInteractive))
            {
                if (playerInput.InteractButtonHold)
                {
                    if (DelaySlider)
                        StartCoroutine(FillSlider(iHoldInteractive));
                }
                else
                {
                    Slider.value = 0;
                }
            }

        }
        else
        {
            indicator.SetActive(false);
            UseProps = false;
            Slider.value = 0;
        }

        if (Slider.value == 0)
            SliderGameObject.SetActive(false);
        else
            SliderGameObject.SetActive(true);
    }

    private void LockMove()
    {
        if(UseProps)
        {
            MoveScript.enabled = false;
            CameraScript.enabled = false;
        }else
        {
            MoveScript.enabled = true;
            CameraScript.enabled = true;
        }
    }

    private IEnumerator FillSlider(IHoldInteractive iHoldInteractive)
    {
        DelaySlider = false;
        Slider.value += 0.1f;
        if(Slider.value >= 1f)
        {
            Slider.value = 0;
            iHoldInteractive.ActiveHold();
        }
        yield return new WaitForSeconds(0.1f);
        DelaySlider = true;
    }
}
