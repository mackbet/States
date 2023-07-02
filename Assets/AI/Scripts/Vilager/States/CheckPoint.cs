using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AI States/CheckPoint")]
public class CheckPoint : AIState
{
    [SerializeField] private Building targetBuilding;
    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);

        _stateMachine.CollideEventHandler.onCollide.AddListener(ReachedPoint);

        ChoosePoint();
    }
    private void ChoosePoint()
    {
        List<Building> buildings = _stateMachine.CityBuilder.Built;
        List<Building> borderBuildings = new List<Building>();

        foreach (Building building in buildings)
        {
            if (_stateMachine.CityBuilder.IsBorderBuilding(building))
                borderBuildings.Add(building);
        }

        targetBuilding = borderBuildings[Random.Range(0, borderBuildings.Count)];
        Vector3 targetPosition = targetBuilding.transform.position;

        _stateMachine.Movement.MoveTo(targetPosition, "CheckPoint");
    }

    private void ReachedPoint(Collision collision)
    {
        if (targetBuilding.transform == collision.transform)
        {
            _stateMachine.Movement.StopMoving("CheckPoint");
            FinishState();
        }
    }
}
