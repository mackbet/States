using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketSelection : MonoBehaviour
{
    public Market Market { get; private set; }
    [SerializeField] private Inventory _inventory;
    [SerializeField] private InventoryVisual _inventoryVisual;
    [SerializeField] private MarketScanner _marketScanner;
    [SerializeField] private GameObject _buttons;

    public Action<Market> OnMarketSet;
    private void OnEnable()
    {
        _marketScanner.OnMarketSelected += SelectMarket;
        _marketScanner.OnMarketDeselected += DeselectMarket;
    }
    private void OnDisable()
    {
        _marketScanner.OnMarketSelected -= SelectMarket;
        _marketScanner.OnMarketDeselected -= DeselectMarket;
    }

    private void SelectMarket(Market market)
    {
        Market = market;
        _inventoryVisual.SetValidItems(new List<AssetItem> { market.Item });
        _buttons.SetActive(true);

        OnMarketSet?.Invoke(market);
    }
    private void DeselectMarket()
    {
        Market = null;
        _buttons.SetActive(false);
    }

    public void TryToSell()
    {
        if (!Market.CoinsEnough || !_inventory.HasItem(Market.Item))
            return;

        Market.Buy();
        _inventory.ItemSold(Market.Item, Market.BuyPrice);
    }
    public void TryToBuy()
    {
        if (!Market.ItemEnough || !_inventory.CoinsEnough(Market.SellPrice))
            return;

        Market.Sell();
        _inventory.ItemBought(Market.Item, Market.SellPrice);
    }
}
