using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AI States/Patrol")]
public class Patrol : AIComplexState
{
    [SerializeField] AIState _checkPoint;
    [SerializeField] AIState _attackEnemy;
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
        FinishState();
        Debug.Log("Patroled");
    }
}
