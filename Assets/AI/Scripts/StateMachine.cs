using System;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [field: SerializeField] public AttackController AttackController { get; private set; }
    [field: SerializeField] public AIMovement Movement { get; private set; }
    [field: SerializeField] public MarketScanner MarketScanner { get; private set; }
    [field: SerializeField] public Inventory Inventory{ get; private set; }
    [field: SerializeField] public CollideEventHandler CollideEventHandler { get; private set; }
    [field: SerializeField] public TriggerEventHandler TriggerEventHandler { get; private set; }

    [SerializeField] private AIParameters parameters;
    [SerializeField] private AIState _currentState;

    public Action<AIState> OnStateChanged;

    private void Start()
    {
        NextState();
    }
    private void SetState(AIState newState)
    {
        if (_currentState)
        {
            _currentState.OnStateFinished.RemoveListener(NextState);
            Destroy(_currentState);
        }

        var ins = Instantiate(newState);
        _currentState = ins;
        _currentState.OnStateFinished.AddListener(NextState);
        _currentState.Initialize(this);
        OnStateChanged.Invoke(_currentState);
    }
    private void NextState()
    {
        SetState(Brain.ComputeState(parameters));
    }
}
