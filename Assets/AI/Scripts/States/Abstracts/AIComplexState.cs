using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AIComplexState : AIState
{
    public AIState CurrentState { get; protected set; } = null;

    public override void Initialize(StateMachine aiCharacter)
    {
        base.Initialize(aiCharacter);
    }
    public void SetState(AIState newState, UnityAction action)
    {
        if (CurrentState)
        {
            CurrentState.OnStateFinished.RemoveAllListeners();
            Destroy(CurrentState);
        }

        var ins = Instantiate(newState);
        ins.OnStateFinished.AddListener(action);
        CurrentState = ins;
        CurrentState.Initialize(_stateMachine);
    }
}
