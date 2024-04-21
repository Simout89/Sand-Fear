using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public class FsmMonster : MonoBehaviour
{
    [SerializeField] private Transform[] PatrolTargets;
    [SerializeField] private bool GizmosActive = false;
    [SerializeField] private float PlayerDetectDistance = 5f;
    [SerializeField] private Animator animator;

    private NavMeshAgent navMeshAgent;
    private Fsm _fsm;

    private GameObject Player;
    [Inject]
    public void Construct(GameObject Player)
    {
        this.Player = Player;
    }

    private CheckPointSystem checkPointSystem;
    [Inject]
    public void Construct(CheckPointSystem checkPointSystem)
    {
        this.checkPointSystem = checkPointSystem;
    }
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        _fsm = new Fsm();

        _fsm.AddState(new FsmStateIdle(_fsm));
        _fsm.AddState(new FsmStatePatrol(_fsm, PatrolTargets, navMeshAgent, transform, Player.transform, PlayerDetectDistance, animator));
        _fsm.AddState(new FsmStatePurSuit(_fsm, navMeshAgent, Player.transform, animator));
        _fsm.AddState(new FsmStateLookingAround(_fsm, transform, Player.transform, PlayerDetectDistance, animator));

        _fsm.SetState<FsmStateIdle>();
    }

    private void Update()
    {
        _fsm.Update();
    }

    private void OnDrawGizmos()
    {
        if(GizmosActive)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, PlayerDetectDistance);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            animator.SetTrigger("Attack");
            checkPointSystem.ReturnOnCheckPoint();
            _fsm.SetState<FsmStatePatrol>();
        }
    }
}
