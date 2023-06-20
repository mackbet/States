using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int Coins => coins;
    [SerializeField] private int coins;
    [field:SerializeField] public List<AssetItem> Items { get; private set; }

    public Action<List<AssetItem>> OnItemsUpated;
    public void AddItem(AssetItem item)
    {
        Items.Add(item);
        OnItemsUpated?.Invoke(Items);
    }

    public void ItemSold(AssetItem item, int income)
    {
        Items.Remove(item);
        coins += income;

        OnItemsUpated?.Invoke(Items);
    }
    public void ItemBought(AssetItem item, int cost)
    {
        Items.Add(item);
        coins -= cost;

        OnItemsUpated?.Invoke(Items);
    }

    public bool CoinsEnough(int value)
    {
        return coins >= value;
    }

    public bool HasItem(AssetItem item)
    {
        return Items.FindIndex(x => x == item) != -1;
    }
}
