using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class FsmStatePatrol : FsmState
{
    protected readonly Transform[] PatrolTargets;
    protected readonly Transform Transform;
    protected readonly Transform PlayerTransform;
    protected readonly NavMeshAgent NavMeshAgent;
    protected readonly Animator Animator;
    protected readonly float PlayerDetectDistance;
    public FsmStatePatrol(Fsm fsm, Transform[] patrolTargets, NavMeshAgent navMeshAgent,
        Transform transform, Transform playerTransform, float playerDetectDistance,
        Animator animator) : base(fsm) 
    {
        PatrolTargets = patrolTargets;
        NavMeshAgent = navMeshAgent;
        PlayerDetectDistance = playerDetectDistance;
        PlayerTransform = playerTransform;
        Transform = transform;
        Animator = animator;
    }

    public override void Enter()
    {
        Debug.Log("Patrol Enter");
        NavMesAgentSetRandomDestination();
    }

    public override void Exit()
    {
        Debug.Log("Patrol Exit");
    }

    public override void Update()
    {
        Patrol();
        CheckDistanceToPlayer();
        Debug.Log("Patrol Update");
    }

    private void Patrol()
    {
        if(NavMeshAgent.remainingDistance < 2f) 
        {
            Fsm.SetState<FsmStateLookingAround>();
        }
    }

    private Vector3 GetRandomTarget()
    {
        return PatrolTargets[Random.Range(0, PatrolTargets.Length)].position;
    }


    private void NavMesAgentSetRandomDestination()
    {
        NavMeshAgent.SetDestination(GetRandomTarget());
    }

    private void CheckDistanceToPlayer()
    {
        if (PlayerDetectDistance > Vector3.Distance(Transform.position, PlayerTransform.position))
        {
            Fsm.SetState<FsmStatePurSuit>();
        }
    }
}
