using System.Collections;
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
    [SerializeField] private PlayerMovement MoveScript;
    [SerializeField] private PlayerCameraController CameraScript;
    [SerializeField] private CameraShake CameraShake;
    private GameObject HoldItem = null;
    private GameObject BufferItem = null;
    private GameObject BufferButton = null;
    private GameObject BufferValue = null;
    private bool DelaySlider = true;
    public bool UseProps { get; private set; } = false;
    private PlayerInput playerInput;
    [Inject]
    public void Construct(PlayerInput playerInput)
    {
        this.playerInput = playerInput;
    }

    private PlayerLocation playerLocation;
    [Inject]
    public void Construct(PlayerLocation playerLocation)
    {
        this.playerLocation = playerLocation;
    }

    private void ValueProps(RaycastHit hit)
    {
        if (playerInput.InteractButton && hit.collider.TryGetComponent(out IValueProps iValueProps) && (HoldItem == null) && hit.collider.TryGetComponent(out IInteractive iInteractive))
        {
            if(iValueProps.index == 0)
            {
                if (BufferValue == null)
                {
                    BufferValue = hit.collider.gameObject;
                    iInteractive.Active = !iInteractive.Active;
                    iInteractive.Activate();
                    UseProps = !UseProps;
                    LockMove();
                }
                else if (BufferValue != null && (BufferValue == hit.collider.gameObject))
                {
                    iInteractive.Active = !iInteractive.Active;
                    iInteractive.Activate();
                    UseProps = !UseProps;
                    LockMove();
                    BufferValue = null;
                }
                else if (BufferValue != null && (BufferValue != hit.collider.gameObject))
                {
                    if (BufferValue.TryGetComponent(out IInteractive BufferiValueProps))
                    {
                        BufferiValueProps.Active = !BufferiValueProps.Active;
                        BufferiValueProps.Activate();
                    }
                    iInteractive.Active = !iInteractive.Active;
                    iInteractive.Activate();
                    BufferValue = hit.collider.gameObject;
                }
            }else
            {
                iInteractive.Active = !iInteractive.Active;
                iInteractive.Activate();
                UseProps = !UseProps;
                LockMove();
                LockMouse();
            }
        }
    }

    private void HoldInteractive(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out IHoldInteractive iHoldInteractive))
        {
            if (playerInput.InteractButtonHold && (BufferValue == null))
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

                HoldItem.transform.parent = hit.collider.transform;
                if (HoldItem.TryGetComponent(out ICollectable iCollectable))
                {
                    HoldItem.transform.localScale = iCollectable.ShelfScale;
                    HoldItem.transform.localPosition = iCollectable.ShelfPos;
                }
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
                HoldItem.transform.parent = ArmSlot.transform;
                if (HoldItem.TryGetComponent(out ICollectable iCollectable))
                {
                    HoldItem.transform.localPosition = iCollectable.PlayerPos;
                    HoldItem.transform.localScale = iCollectable.PlayerScale;
                    HoldItem.transform.localRotation = Quaternion.Euler(iCollectable.PlayerRottation);
                }
                iShelf.Item = null;
                OnHoldItem.Invoke();
            }
            else if (iShelf.Item != null && (HoldItem != null))
            {
                BufferItem = iShelf.Item; // буффер

                iShelf.Item = HoldItem;
                if (iShelf.Item.TryGetComponent(out IInteractive iInteractiveP2PShelf))
                    iInteractiveP2PShelf.Active = false;
                HoldItem.transform.parent = hit.collider.transform;
                if (HoldItem.TryGetComponent(out ICollectable iCollectableShelf))
                {
                    HoldItem.transform.localScale = iCollectableShelf.ShelfScale;
                    HoldItem.transform.localPosition = iCollectableShelf.ShelfPos;
                }
                HoldItem.transform.localRotation = Quaternion.Euler(0, 180, 0); // кладем на полку


                HoldItem = BufferItem;
                if (HoldItem.TryGetComponent(out IInteractive iInteractiveP2PPlayer))
                    iInteractiveP2PPlayer.Active = true;

                HoldItem.transform.parent = ArmSlot.transform;
                if (HoldItem.TryGetComponent(out ICollectable iCollectable))
                {
                    HoldItem.transform.localPosition = iCollectable.PlayerPos;
                    HoldItem.transform.localScale = iCollectable.PlayerScale;
                    HoldItem.transform.localRotation = Quaternion.Euler(iCollectable.PlayerRottation);
                } // Забираем из буффера

                BufferItem = null;

            }
        }
    }

    private void CollectItem(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out ICollectable iCollectable) && (HoldItem == null) && playerInput.InteractButton&& hit.collider.TryGetComponent(out IInteractive iInteractive))
        {
            iInteractive.Active = true;
            iCollectable.Collect();
            hit.collider.transform.parent = ArmSlot.transform;
            hit.collider.transform.localPosition = iCollectable.PlayerPos;
            hit.collider.transform.localRotation = Quaternion.Euler(iCollectable.PlayerRottation);
            HoldItem = hit.collider.gameObject;
            OnHoldItem.Invoke();
        }
    }

    private void PutItem()
    {
        if (playerInput.SecondInteractButton && HoldItem && (playerLocation.GetLocation() != PlayerLocation.Locations.Car))
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

    private void Button(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out IButton iButton))
        {
            if (playerInput.InteractButtonHold)
            {
                if(BufferButton == null)
                    BufferButton = hit.collider.gameObject;
                else
                {
                    BufferButton.GetComponent<IButton>().Relize();
                    BufferButton = hit.collider.gameObject;
                }
                iButton.Press();
            }
            else
            {
                BufferButton = hit.collider.gameObject;
                iButton.Relize();
            }
        }
    }

    private void Update()
    {

        RaycastHit hit;

        PutItem();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Distance))
        {
            if(hit.collider.TryGetComponent(out IInteractive interactive))
                indicator.SetActive(true);
            else
                indicator.SetActive(false);
            ValueProps(hit);

            HoldInteractive(hit);

            CollectItem(hit);

            Shelf(hit);

            Button(hit);


            if ((BufferButton != null && !playerInput.InteractButtonHold) || (playerInput.InteractButtonHold && (BufferButton != null) && hit.collider.gameObject != BufferButton))
            {
                BufferButton.GetComponent<IButton>().Relize();
                BufferButton = null;
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




        if(playerInput.InteractButtonItems && HoldItem && HoldItem.TryGetComponent(out IUseableItem iUseableItem))
        {
            iUseableItem.Use();
        }
    }

    private void LockMove()
    {
        if(UseProps)
        {
            MoveScript.enabled = false;
            CameraShake.enabled = false;
        }
        else
        {
            MoveScript.enabled = true;
            CameraShake.enabled = true;
        }
    }

    private void LockMouse()
    {
        if (UseProps)
        {
            CameraScript.enabled = false;
            Time.timeScale = 0;
        }
        else
        {
            CameraScript.enabled = true;
            Time.timeScale = 1;
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
