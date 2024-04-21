using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FsmStateLookingAround : FsmState
{
    protected readonly Transform PlayerTransform;
    protected readonly Transform Transform;
    protected readonly Animator Animator;
    protected readonly float PlayerDetectDistance;
    private float animationClipLenght = 2.5f;
    private float timeLeft;

    public FsmStateLookingAround(Fsm fsm, Transform transform, Transform playerTransform, float playerDetectDistance,
        Animator animator) : base(fsm)
    {
        PlayerTransform = playerTransform;
        Animator = animator;
        PlayerDetectDistance = playerDetectDistance;
        Transform = transform;
    }

    public override void Enter()
    {
        Debug.Log("LookingAround Enter");
        Animator.SetTrigger(Convert.ToString((AnimationClip)Random.Range(0, 2)));
        timeLeft = animationClipLenght;
    }

    public override void Exit()
    {
        Debug.Log("LookingAround Exit");
        Animator.SetTrigger("Patrol");
    }

    public override void Update()
    {
        Debug.Log("LookingAround Update");
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            Fsm.SetState<FsmStatePatrol>();
        }
        CheckDistanceToPlayer();
    }
    private void CheckDistanceToPlayer()
    {
        if (PlayerDetectDistance > Vector3.Distance(Transform.position, PlayerTransform.position))
        {
            Fsm.SetState<FsmStatePurSuit>();
        }
    }


    private enum AnimationClip
    {
        Idle1, Idle2
    }
}
