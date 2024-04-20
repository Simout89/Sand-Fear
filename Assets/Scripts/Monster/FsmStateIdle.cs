using UnityEngine;

public class FsmStateIdle : FsmState
{
    public FsmStateIdle(Fsm fsm) : base(fsm) { }

    public override void Enter()
    {
        Debug.Log("Idle Enter");
        Fsm.SetState<FsmStatePatrol>();
    }

    public override void Exit()
    {
        Debug.Log("Idle Exit");
    }

    public override void Update()
    {
        Debug.Log("Idle Update");
    }

}
