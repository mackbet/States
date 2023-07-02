using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AI States/GoLootSpawner")]
public class GoLootSpawner : AIState
{
    [field:SerializeField] public LootSpawner TargetSpawner { get; private set; }

    [SerializeField] private AssetItem Item;

    public override void Initialize(StateMachine aiCharacter)
    {
        base.Initialize(aiCharacter);

        _stateMachine.TriggerEventHandler.onTrigger.AddListener(ReachedStone);
        ChooseSpawner();
    }
    private void ChooseSpawner()
    {
        TargetSpawner = AIResourceMap.GetClosestSpawner(_stateMachine.transform.position, Item);
        if (!TargetSpawner)
        {
            isFailed = true;
            //Debug.Log("There are not stone");
            FinishState();
        }
        else if (_stateMachine.AttackController.InAttackZone(TargetSpawner.transform.position))
        {
            FinishState();
        }
        else
        {
            TargetSpawner.HealthObject.OnDead += SpawnerStolen;
            Vector3 targetPosition = TargetSpawner.transform.position;
            Vector3 direction = TargetSpawner.transform.position - _stateMachine.transform.position;
            targetPosition -= (direction.normalized * (_stateMachine.AttackController.Weapon.AttackRange / 3));

            _stateMachine.Movement.MoveTo(targetPosition, "GoStone");
        }
    }
    private void SpawnerStolen()
    {
        TargetSpawner.HealthObject.OnDead -= SpawnerStolen;
        ChooseSpawner();
    }
    private void ReachedStone(Collider collider)
    {
        if (TargetSpawner && TargetSpawner.transform == collider.transform)
        {
            TargetSpawner.HealthObject.OnDead -= SpawnerStolen;

            _stateMachine.Movement.StopMoving("GoStone");


            FinishState();
        }
    }

}
