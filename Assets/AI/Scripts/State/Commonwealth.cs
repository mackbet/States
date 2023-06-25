using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Commonwealth : MonoBehaviour
{
    [SerializeField] private List<ResourceData> _resourcesList;
    [SerializeField] private List<Market> _builtMarkets;

    [field: SerializeField] public int CoinsCount { get; private set; }

    public UnityEvent<AssetItem> OnItemGot;
    private void Awake()
    {
        Initialize();
    }
    public void AddMarket(Market market)
    {
        _builtMarkets.Add(market);
    }
    public Market GetMarket(AssetItem item, Vector3 position)
    {
        float? minDistance = null;
        Market selectedMarket = null;
        foreach (Market market in _builtMarkets)
        {
            if (!market)
                RemoveMarket(market);
            else if (market.Item == item)
            {
                float currentDistance = Vector3.Distance(position, market.transform.position);

                if (minDistance == null || minDistance > currentDistance)
                {
                    minDistance = currentDistance;
                    selectedMarket = market;
                }
            }
        }
        return selectedMarket;
    }
    public void GetTax(int value)
    {
        CoinsCount += value;
    }
    public int GetItemCount(AssetItem item)
    {
        int result = _resourcesList.Find((x)=> x.item == item).Count;
        return result;
    }
    public int GetItemBuyPrice(AssetItem item)
    {
        int result = _resourcesList.Find((x) => x.item == item).BuyPrice;
        return result;
    }
    public int GetItemSellPrice(AssetItem item)
    {
        int result = _resourcesList.Find((x) => x.item == item).SellPrice;
        return result;
    }
    public void ReduceResource(AssetItem item, int count)
    {
        _resourcesList.Find((x) => x.item == item).Count -= count;
    }
    private void RemoveMarket(Market market)
    {
        _builtMarkets.Remove(market);
    }
    private void Initialize()
    {
        foreach (Market market in _builtMarkets)
        {
            market.OnItemBought.AddListener(ItemBought);
            market.OnItemSold.AddListener(ItemSold);
        }
    }
    private void ItemBought(AssetItem item, int value)
    {
        _resourcesList.Find((x) => x.item == item).Count += 1;
        CoinsCount -= value;
        OnItemGot.Invoke(item);
    }
    private void ItemSold(AssetItem item, int value)
    {
        _resourcesList.Find((x) => x.item == item).Count -= 1;
        CoinsCount += value;
    }

    [Serializable]
    private class ResourceData
    {
        [field: SerializeField] public AssetItem item { get; private set; }
        [field: SerializeField] public int Count { get; set; }
        [field: SerializeField] public int BuyPrice { get; private set; }
        [field: SerializeField] public int SellPrice { get; private set; }
    }
}