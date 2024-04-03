using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class PlayerInteract : MonoBehaviour
{
    public static UnityEvent OnHoldItem = new UnityEvent();


    [SerializeField] private float Distance = 1f;
    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject SliderGameObject;
    [SerializeField] private GameObject ArmSlot;
    [SerializeField] private Transform camera;
    [SerializeField] private Slider Slider;
    [SerializeField] private float Coficent = 1;
    [SerializeField] private PlayerMovement MoveScript;
    [SerializeField] private PlayerCameraController CameraScript;



    private GameObject HoldItem = null;
    private GameObject BufferItem = null;


    private bool DelaySlider = true;
    public bool UseProps { get; private set; } = false;
    private PlayerInput playerInput;
    [Inject]
    public void Construct(PlayerInput playerInput)
    {
        this.playerInput = playerInput;
    }

    private void ValueProps(RaycastHit hit, IInteractive iInteractive)
    {
        if (playerInput.InteractButton && hit.collider.TryGetComponent(out IValueProps iValueProps))
        {
            iInteractive.Active = !iInteractive.Active;
            UseProps = !UseProps;
            LockMove();
        }
    }

    private void HoldInteractive(RaycastHit hit)
    {
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

    private void Shelf(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out IShelf iShelf) && playerInput.InteractButton)
        {
            if (iShelf.Item == null && (HoldItem != null)) // кладем
            {
                iShelf.Item = HoldItem;
                HoldItem.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                HoldItem.transform.position = hit.collider.transform.position;
                HoldItem.transform.parent = hit.collider.transform;
                HoldItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
                HoldItem = null;
                OnHoldItem.Invoke();
            }
            else if (iShelf.Item != null && (HoldItem == null)) // Забираем
            {
                HoldItem = iShelf.Item;
                HoldItem.transform.position = ArmSlot.transform.position;
                HoldItem.transform.parent = ArmSlot.transform;
                HoldItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
                HoldItem.transform.localScale = new Vector3(1f, 1f, 1f);
                iShelf.Item = null;
                OnHoldItem.Invoke();
            }
            else if (iShelf.Item != null && (HoldItem != null))
            {
                BufferItem = iShelf.Item;

                iShelf.Item = HoldItem;
                HoldItem.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                HoldItem.transform.position = hit.collider.transform.position;
                HoldItem.transform.parent = hit.collider.transform;
                HoldItem.transform.localRotation = Quaternion.Euler(0, 0, 0);

                HoldItem = BufferItem;
                HoldItem.transform.position = ArmSlot.transform.position;
                HoldItem.transform.parent = ArmSlot.transform;
                HoldItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
                HoldItem.transform.localScale = new Vector3(1f, 1f, 1f);

                BufferItem = null;

            }
        }
    }

    private void CollectItem(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out ICollectable iCollectable) && (HoldItem == null) && playerInput.InteractButton)
        {
            iCollectable.Collect();
            hit.collider.transform.position = ArmSlot.transform.position;
            hit.collider.transform.parent = ArmSlot.transform;
            hit.collider.transform.localRotation = Quaternion.Euler(0, 0, 0);
            HoldItem = hit.collider.gameObject;
            OnHoldItem.Invoke();
        }
    }

    private void PutItem()
    {
        if (playerInput.SecondInteractButton && HoldItem)
        {
            if (HoldItem.TryGetComponent(out ICollectable iCollectable))
                iCollectable.Put();
            HoldItem.transform.parent = null;
            HoldItem = null;
            OnHoldItem.Invoke();
        }
    }

    private void Update()
    {

        RaycastHit hit;

        PutItem();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Distance) && hit.collider.TryGetComponent(out IInteractive iInteractive))
        {
            indicator.SetActive(true);
            ValueProps(hit, iInteractive);

            CollectItem(hit);

            HoldInteractive(hit);

            Shelf(hit);
            
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
