using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AI States/CheckPoint")]
public class CheckPoint : AIState
{
    [SerializeField] private Building targetBuilding;
    [SerializeField] private float minDistance;
    [SerializeField] private float viewRange;
    private Vector3 targetPosition;
    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);

        ChoosePoint();
    }
    private void ChoosePoint()
    {
        List<Building> buildings = _stateMachine.CityBuilder.Built;
        List<Building> borderBuildings = new List<Building>();

        foreach (Building building in buildings)
        {
            if (_stateMachine.CityBuilder.IsBorderBuilding(building) && Vector3.Distance(_stateMachine.transform.position, building.transform.position) > minDistance)
                borderBuildings.Add(building);
        }

        targetBuilding = borderBuildings[Random.Range(0, borderBuildings.Count)];

        if (!targetBuilding)
        {
            FinishState();
        }
        else
        {
            targetPosition = targetBuilding.transform.position;
            Vector3 direction = targetBuilding.transform.position - _stateMachine.transform.position;
            targetPosition += (5 * direction.normalized);

            _stateMachine.Movement.Walk();
            _stateMachine.Movement.MoveTo(targetPosition, "CheckPoint " + targetPosition);
            _stateMachine.StartCoroutine(ReachedPoint());
        }
    }


    private IEnumerator ReachedPoint()
    {
        while(Vector3.Distance(_stateMachine.transform.position, targetPosition) > 1)
        {
            Collider[] colliders = Physics.OverlapSphere(_stateMachine.transform.position, viewRange);

            foreach (Collider collider in colliders)
            {
                if(collider.TryGetComponent(out HealthObject healthObject))
                {
                    if (healthObject.Parameters.TeamId != 0 && healthObject.Parameters.TeamId != _stateMachine.Vilager.Parameters.TeamId)
                    {
                        _stateMachine.Movement.StopMoving("CheckPoint");
                        _stateMachine.Movement.Run();

                        FinishState();
                        yield break;
                    }
                }
            }

            yield return new WaitForSeconds(1f);
        }

        _stateMachine.Movement.StopMoving("CheckPoint");
        _stateMachine.Movement.Run();

        FinishState();
    }
}
