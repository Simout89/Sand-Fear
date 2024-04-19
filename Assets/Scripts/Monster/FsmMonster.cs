using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public class FsmMonster : MonoBehaviour
{
    [SerializeField] private Transform[] PatrolTargets;
    [SerializeField] private float PlayerDetectDistance = 5f;
    [SerializeField] private bool GizmosActive = false;

    private NavMeshAgent navMeshAgent;
    private Fsm _fsm;

    private GameObject Player;
    [Inject]
    public void Construct(GameObject Player)
    {
        this.Player = Player;
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        _fsm = new Fsm();

        _fsm.AddState(new FsmStateIdle(_fsm));
        _fsm.AddState(new FsmStatePatrol(_fsm, PatrolTargets, navMeshAgent, transform, Player.transform, PlayerDetectDistance));
        _fsm.AddState(new FsmStatePurSuit(_fsm, navMeshAgent, Player.transform));

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
}
