using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class OutSideRadar : MonoBehaviour
{
    [SerializeField] private float Distance = 5f;
    [SerializeField] private float Radius = 1f;
    [SerializeField] private float Speed = 1f;
    [SerializeField] private float PointSpawnSpeed = 1f;
    [SerializeField] private float Const = 1f;

    [SerializeField] private GameObject Pivot;
    [SerializeField] private GameObject GreenPoint;
    [SerializeField] private GameObject Target;

    private bool delay = true;
    private void Awake()
    {
        transform.DOLocalRotate(new Vector3(0, 360, 0), Speed, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear).SetRelative();
        Pivot.transform.DOLocalRotate(new Vector3(0, 360, 0), Speed, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear).SetRelative();
    }
    private void Update()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, Radius, transform.forward, out hit, Distance, ~6))
        {
            Target.transform.localPosition = new Vector3(0,-0.062f, -hit.distance * Const);
            if(delay)
            {
                if(hit.collider.gameObject.layer == 3)
                {
                    Instantiate(GreenPoint, Target.transform.position, Quaternion.Euler(0, 0, Pivot.transform.rotation.eulerAngles.z));
                    StartCoroutine(Delay());
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * Distance, Radius);
    }

    private IEnumerator Delay()
    {
        delay = false;
        yield return new WaitForSeconds(PointSpawnSpeed);
        delay = true;
    }
}
