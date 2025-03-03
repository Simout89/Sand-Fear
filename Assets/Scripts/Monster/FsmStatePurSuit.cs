using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class FsmStatePurSuit : FsmState
{
    protected readonly Transform PlayerTransform;
    protected readonly NavMeshAgent NavMeshAgent;
    protected readonly Animator Animator;
    private NavMeshPath navMeshPath;
    public FsmStatePurSuit(Fsm fsm, NavMeshAgent navMeshAgent, Transform playerTransform
        ,Animator animator) : base(fsm) 
    {
        NavMeshAgent = navMeshAgent;
        PlayerTransform = playerTransform;
        Animator = animator;
    }

    public override void Enter()
    {
        Debug.Log("PurSuit Enter");
        navMeshPath = new NavMeshPath();
        Animator.SetTrigger("Pursuit");
        NavMeshAgent.speed += 5f;
    }

    public override void Exit()
    {
        Debug.Log("PurSuit Exit");
    }

    public override void Update()
    {
        Debug.Log("PurSuit Update");
        NavMeshAgentCalculatePath();
    }

    private void NavMeshAgentCalculatePath()
    {
        NavMeshAgent.CalculatePath(PlayerTransform.position, navMeshPath);
        NavMeshAgent.SetPath(navMeshPath);
    }
}
