using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AI States/SafeAttack")]
public class SafeAttack : AIState
{
    [SerializeField] private float _viewRange;
    private AttackController _attackController;
    [SerializeField] private HealthObject _target;
    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);

        _attackController = _stateMachine.AttackController;
        _stateMachine.CollideEventHandler.onCollide.AddListener(EnemyReached);

        Scan();
        TryToKill();
    }
    private void Scan()
    {
        Collider[] colliders = Physics.OverlapSphere(_stateMachine.transform.position, _viewRange);

        HealthObject closestEnemy=null;
        float distance = 0;
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out HealthObject healthObject))
            {
                if (healthObject.Parameters.TeamId != 0 && healthObject.Parameters.TeamId != _stateMachine.Vilager.Parameters.TeamId)
                {
                    float currentDistance = Vector3.Distance(_stateMachine.transform.position, healthObject.transform.position);
                    if (closestEnemy == null || distance > currentDistance)
                    {
                        closestEnemy = healthObject;
                        distance = currentDistance;
                    }
                }
            }
        }
        _target = closestEnemy;

        if (_target)
        {
            _target.OnDead += Killed;
            _attackController.OnAttackRecharged += TryToKill;
        }
    }

    private void TryToKill()
    {
        if (!_target)
            FinishState();
        else
        {
            if (_attackController.InAttackZone(_target.transform.position))
            {
                Hit();
            }
            else
            {
                _stateMachine.Movement.Start—hase(_target.transform);
            }
        }
    }

    private void EnemyReached(Collision collision)
    {
        if(_target && collision.transform.position == _target.transform.position)
        {
            _stateMachine.Movement.StopMoving("SafeAttack");
            TryToKill();
        }
    }
    private void Hit()
    {
        _attackController.TryToAttack(_stateMachine.Vilager.Parameters.TeamId);
    }
    private void Killed()
    {
        _target.OnDead -= Killed;


        Scan();
        TryToKill();
    }
}
