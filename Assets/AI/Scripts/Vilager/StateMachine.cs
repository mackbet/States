using System;
using UnityEngine;
using UnityEngine.Events;

public class StateMachine : MonoBehaviour
{
    [field: SerializeField] public AttackController AttackController { get; private set; }
    [field: SerializeField] public AIMovement Movement { get; private set; }
    [field: SerializeField] public MarketScanner MarketScanner { get; private set; }
    [field: SerializeField] public Inventory Inventory{ get; private set; }
    [field: SerializeField] public CollideEventHandler CollideEventHandler { get; private set; }
    [field: SerializeField] public TriggerEventHandler TriggerEventHandler { get; private set; }
    [field: SerializeField] public AIParameters Parameters { get; private set; }
    [field: SerializeField] public Commonwealth Ñommonwealth { get; private set; }

    [SerializeField] private AIState _currentState;

    [HideInInspector] public UnityEvent<AIState> OnStateFinished;
    [HideInInspector] public UnityEvent<AIState> OnStateChanged;

    public bool RandomStates;

    public void SetState(AIState newState)
    {
        var ins = Instantiate(newState);
        _currentState = ins;
        _currentState.OnStateFinished.AddListener(StateFinished);
        _currentState.Initialize(this);
        OnStateChanged.Invoke(_currentState);
    }
    private void StateFinished()
    {
        OnStateFinished.Invoke(_currentState);
        _currentState.OnStateFinished.RemoveListener(StateFinished);

        Destroy(_currentState);
    }

    
}
