using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AI States/GoMarket")]
public class GoMarket : AIState
{
    [field:SerializeField] public Market Market { get; private set; }
    [SerializeField] private AssetItem assetItem;

    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);

        _stateMachine.MarketScanner.OnMarketSelected += ReachedMarket;

        ChooseMarket();
    }
    private void ChooseMarket()
    {
        Market = _stateMachine.Ñommonwealth.GetMarket(assetItem, _stateMachine.transform.position);
        if (!Market || _stateMachine.MarketScanner.TargetMarket == Market)
        {
            _stateMachine.MarketScanner.OnMarketSelected -= ReachedMarket;
            _stateMachine.Movement.StopMoving("GoMarket");
            FinishState();
        }
        else
        {
            if(Market.TryGetComponent(out Building building))
                building.OnDead += ChooseMarket;

            Vector3 targetPosition = Market.transform.position;
            Vector3 direction = Market.transform.position - _stateMachine.transform.position;
            targetPosition -= (direction.normalized * 4f);
            _stateMachine.Movement.MoveTo(targetPosition, "GoMarket");
        }

    }
    private void ReachedMarket(Market market)
    {
        if (Market == market)
        {
            _stateMachine.Movement.StopMoving("GoMarket");

            if (Market.TryGetComponent(out Building building))
                building.OnDead += ChooseMarket;

            _stateMachine.MarketScanner.OnMarketSelected -= ReachedMarket;
            _stateMachine.Movement.StopMoving("GoMarket");
            FinishState();
        }
    }
}
