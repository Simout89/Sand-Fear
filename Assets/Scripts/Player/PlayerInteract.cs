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
                if (HoldItem.TryGetComponent(out IInteractive iInteractiveP2PShelf))
                    iInteractiveP2PShelf.Active = false;
                HoldItem.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                HoldItem.transform.position = hit.collider.transform.position;
                HoldItem.transform.parent = hit.collider.transform;
                HoldItem.transform.localRotation = Quaternion.Euler(0, 180, 0);
                HoldItem = null;
                OnHoldItem.Invoke();
            }
            else if (iShelf.Item != null && (HoldItem == null)) // Забираем
            {
                HoldItem = iShelf.Item;
                if (HoldItem.TryGetComponent(out IInteractive iInteractiveP2PShelf))
                    iInteractiveP2PShelf.Active = true;
                HoldItem.transform.position = ArmSlot.transform.position;
                HoldItem.transform.parent = ArmSlot.transform;
                HoldItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
                HoldItem.transform.localScale = new Vector3(1f, 1f, 1f);
                iShelf.Item = null;
                OnHoldItem.Invoke();
            }
            else if (iShelf.Item != null && (HoldItem != null))
            {
                BufferItem = iShelf.Item; // буффер

                iShelf.Item = HoldItem;
                if (iShelf.Item.TryGetComponent(out IInteractive iInteractiveP2PShelf))
                    iInteractiveP2PShelf.Active = false;
                HoldItem.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                HoldItem.transform.position = hit.collider.transform.position;
                HoldItem.transform.parent = hit.collider.transform;
                HoldItem.transform.localRotation = Quaternion.Euler(0, 180, 0); // кладем на полку


                HoldItem = BufferItem;
                if (HoldItem.TryGetComponent(out IInteractive iInteractiveP2PPlayer))
                    iInteractiveP2PPlayer.Active = true;
                HoldItem.transform.position = ArmSlot.transform.position;
                HoldItem.transform.parent = ArmSlot.transform;
                HoldItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
                HoldItem.transform.localScale = new Vector3(1f, 1f, 1f); // забираем из буффера

                BufferItem = null;

            }
        }
    }

    private void CollectItem(RaycastHit hit, IInteractive iInteractive)
    {
        if (hit.collider.TryGetComponent(out ICollectable iCollectable) && (HoldItem == null) && playerInput.InteractButton)
        {
            iInteractive.Active = true;
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
            if (HoldItem.TryGetComponent(out IInteractive iInteractive))
                iInteractive.Active = false;
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

            HoldInteractive(hit);

            CollectItem(hit, iInteractive);

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
