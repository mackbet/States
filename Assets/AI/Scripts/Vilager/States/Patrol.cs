using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AI States/Patrol")]
public class Patrol : AIComplexState
{
    [SerializeField] AIState _checkPoint;
    [SerializeField] AIState _safeAttack;
    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);

        GoPoint();
    }
    private void GoPoint()
    {
        SetState(_checkPoint, AttackEnemy);
    }
    public void AttackEnemy()
    {
        SetState(_safeAttack, FinishState);
    }
}
