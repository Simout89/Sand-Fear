using UnityEngine;
using Zenject;

public class CarBodyRotate : MonoBehaviour
{
    private CarController carController;
    [Inject]
    public void Construct(CarController carController)
    {
        this.carController = carController;
    }

    [SerializeField] private Transform body;

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (((carController.leverVertical.Value == 0)
            && (carController.leverHorizontal.Value == 0))
            && Physics.Raycast(transform.position, -transform.up, out hit))
        {
            Vector3 surfaceNormal = hit.normal;
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, surfaceNormal) * transform.rotation;
            body.transform.rotation = targetRotation;
        }
    }
}
