using DG.Tweening;
using System.Collections;
using UnityEngine;
using Zenject;

public class OutSideRadar : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    [SerializeField] private Lever leverVertical;
    [SerializeField] private float Distance = 5f;
    [SerializeField] private float Radius = 1f;
    [SerializeField] private float Speed = 1f;
    [SerializeField] private float PointSpawnSpeed = 1f;
    [SerializeField] private float PingDelaySec = 1f;
    [SerializeField] private float DetectAngel = 1f;
    [SerializeField] private GameObject Pivot;
    [SerializeField] private GameObject GreenPoint;
    [SerializeField] private GameObject YellowPoint;
    [SerializeField] private GameObject Target;
    [SerializeField] private AudioClip RadarPing;
    [SerializeField] private AudioSource audioSource;


    private PlayerLocation playerLocation;
    [Inject]
    public void Construct(PlayerLocation playerLocation)
    {
        this.playerLocation = playerLocation;
    }

    private bool delay = true;
    private bool pingDelay = true;
    private void Awake()
    {
        transform.DOLocalRotate(new Vector3(0, 360, 0), Speed, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear).SetRelative();
        Pivot.transform.DOLocalRotate(new Vector3(0, 360, 0), Speed, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear).SetRelative();
    }
    private void Update()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, Radius, transform.forward, out hit, leverVertical.Value, ~layer))
        {
            Target.transform.localPosition = new Vector3(0,-0.062f, (hit.distance/ leverVertical.Value) * -1.05f);
            if(delay)
            {
                if(hit.collider.gameObject.layer == 3)
                {
                    Vector3 surfaceNormal = hit.normal;
                    if(Mathf.Abs(surfaceNormal.x) > DetectAngel) // реяр
                    {
                        Instantiate(YellowPoint, Target.transform.position, Quaternion.Euler(0, 0, Pivot.transform.rotation.eulerAngles.z));
                        StartCoroutine(Delay());
                    }
                }else
                {
                    if(pingDelay && (playerLocation.GetLocation() == PlayerLocation.Locations.Car))
                    {
                        audioSource.PlayOneShot(RadarPing);
                        StartCoroutine(PingDelay());
                    }
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
    private IEnumerator PingDelay()
    {
        pingDelay = false;
        yield return new WaitForSeconds(PingDelaySec);
        pingDelay = true;
    }
}
