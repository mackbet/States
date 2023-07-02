using UnityEngine;
using UnityEngine.Events;

public abstract class AIComplexState : AIState
{
    [field:SerializeField] public AIState CurrentState { get; protected set; } = null;

    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
    }
    public void SetState(AIState newState, UnityAction action)
    {
        if (CurrentState)
        {
            CurrentState.OnStateFinished.RemoveAllListeners();
            if (CurrentState.isFailed)
                isFailed = true;

            Destroy(CurrentState);
        }

        var ins = Instantiate(newState);
        ins.OnStateFinished.AddListener(action);
        CurrentState = ins;
        CurrentState.Initialize(_stateMachine);
    }
}
