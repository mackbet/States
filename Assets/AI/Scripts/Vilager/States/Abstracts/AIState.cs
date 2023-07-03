using UnityEngine;
using UnityEngine.Events;

public abstract class AIState : ScriptableObject
{
    [field: SerializeField] public Sprite UISprite { get; private set; }
    [HideInInspector] public UnityEvent OnStateFinished;
    public bool isFailed { get; protected set; }
    public bool isFinished { get; private set; }
    protected StateMachine _stateMachine = null;
    public virtual void Initialize(StateMachine stateMachine)
    {
        //Debug.Log(name + " started");
        _stateMachine = stateMachine;
    }

    public void FinishState()
    {
        isFinished = true;
        //Debug.Log(name + " finished");
        OnStateFinished.Invoke();
        Destroy(this);
    }
}


