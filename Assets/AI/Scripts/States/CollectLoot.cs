using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AI States/CollectLoot")]
public class CollectLoot : AIState
{
    [SerializeField] private float _viewDistance;

    [SerializeField] private List<Loot> _lootList = new List<Loot>();
    public override void Initialize(StateMachine _stateMachine)
    {
        base.Initialize(_stateMachine);

        _stateMachine.Movement.OnChased.AddListener(Collect);

        Scan();
        Collect();
    }

    private void Scan()
    {
        Collider[] colliders = Physics.OverlapSphere(_stateMachine.transform.position, _viewDistance);
        
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Loot loot))
            {
                _lootList.Add(loot);
            }
        }
    }

    private void Collect()
    {
        if (_lootList.Count > 0)
        {
            if (!_lootList[0])
            {
                _lootList.RemoveAt(0);
                Collect();
                return;
            }

            _lootList[0].OnTaken.AddListener((value) => { Collect(); });
            _stateMachine.Movement.Start—hase(_lootList[0].transform);
            _lootList.RemoveAt(0);
        }
        else
        {
            _stateMachine.Movement.OnChased.RemoveAllListeners();
            FinishState();
        }
    }
}
