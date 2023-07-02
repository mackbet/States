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
    [field: SerializeField] public Parameters Parameters => _vilager.Parameters;
    [field: SerializeField] public Commonwealth Commonwealth { get; set; }
    [field: SerializeField] public CityBuilder CityBuilder { get; set; }

    [field:SerializeField] public AIState CurrentState { get; private set; }

    [HideInInspector] public UnityEvent<AIState> OnStateFinished;
    [HideInInspector] public UnityEvent<AIState> OnStateChanged;

    [SerializeField] private Vilager _vilager;

    public void Start()
    {
        SetState(Brain.ComputeState(this));
    }

    public void SetState(AIState newState)
    {
        var ins = Instantiate(newState);
        CurrentState = ins;
        CurrentState.OnStateFinished.AddListener(StateFinished);
        CurrentState.Initialize(this);
        OnStateChanged.Invoke(CurrentState);
    }
    private void StateFinished()
    {
        OnStateFinished.Invoke(CurrentState);
        CurrentState.OnStateFinished.RemoveListener(StateFinished);

        SetState(Brain.ComputeState(this));
    }

    
}
