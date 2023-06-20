using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AI States/CrushHealthObject")]
public class CrushHealthObject : AIState 
{
    private AttackController _attackController;
    public LootSpawner TargetSpawner { get; private set; }
    [SerializeField] private AssetItem Item;
    public override void Initialize(StateMachine aiCharacter)
    {
        base.Initialize(aiCharacter);

        TargetSpawner = AIResourceMap.GetClosestSpawner(_stateMachine.transform.position, Item);

        if (!TargetSpawner)
        {
            Debug.LogError("Closest Stone is null");
            FinishState();
        }
        else if (!_stateMachine.AttackController.InAttackZone(TargetSpawner.transform.position))
        {
            Debug.LogError("Closest Stone is not in attack zone");
            FinishState();
        }
        else
        {
            _attackController = _stateMachine.AttackController;

            TargetSpawner.HealthObject.OnDead += StoneCrushed;
            _attackController.onAttackRecharged += Hit;
            Hit();
        }
    }

    public void Hit()
    {
        _attackController.TryToAttack();
    }

    public void StoneCrushed()
    {
        _attackController.onAttackRecharged -= Hit;

        FinishState();
    }
}
