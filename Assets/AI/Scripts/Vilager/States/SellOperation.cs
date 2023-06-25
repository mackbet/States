using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject/AI States/SellOperation")]
public class SellOperation : AIState
{
    [SerializeField] private AssetItem assetItem;
    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);

        Market market = _stateMachine.MarketScanner.TargetMarket;

        if (market && market.Item==assetItem)
        {
            if (market.CoinsEnough && stateMachine.Inventory.HasItem(assetItem))
            {
                market.Buy();
                stateMachine.Inventory.ItemSold(market.Item, market.BuyPrice);
            }
        }

        FinishState();
    }
}
