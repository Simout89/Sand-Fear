using System;
using System.Collections.Generic;

public class Fsm
{
    private FsmState StateCurrent { get; set; }

    private Dictionary<Type, FsmState> _state = new Dictionary<Type, FsmState>();

    public void AddState(FsmState state)
    {
        _state.Add(state.GetType(), state);
    }

    public void SetState<T>() where T : FsmState
    {
        var type = typeof(T);

        if(StateCurrent != null && StateCurrent.GetType() == type)
        {
            return;
        }

        if(_state.TryGetValue(type, out var newState))
        {
            StateCurrent?.Exit();

            StateCurrent = newState;

            StateCurrent.Enter();
        }

    }

    public void Update()
    {
        StateCurrent?.Update();
    }
}
