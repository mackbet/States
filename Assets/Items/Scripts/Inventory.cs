using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public int Coins
    {
        get
        {
            return coins;
        }
        set
        {
            int delta = value - coins;
            coins = value;

            if (delta > 0)
            {
                OnCoinGot.Invoke(delta);
            }
        }
    }
    public bool IsEmpty => Items.Count == 0;

    [SerializeField] private int coins;
    [field:SerializeField] public List<AssetItem> Items { get; private set; }

    public Action<List<AssetItem>> OnItemsUpated;

    public UnityEvent<AssetItem> OnItemGot;
    public UnityEvent<int> OnCoinGot;
    public void AddItem(AssetItem item)
    {
        Items.Add(item);
        OnItemGot?.Invoke(item);
        OnItemsUpated?.Invoke(Items);
    }
    public void ItemSold(AssetItem item, int income)
    {
        Items.Remove(item);
        Coins += income;

        OnItemsUpated?.Invoke(Items);
    }
    public void ItemBought(AssetItem item, int cost)
    {
        Items.Add(item);
        Coins -= cost;

        OnItemsUpated?.Invoke(Items);
    }
    public void PayTaxes(int cost)
    {
        Coins -= cost;
    }
    public bool CoinsEnough(int value)
    {
        return Coins >= value;
    }
    public bool HasItem(AssetItem item)
    {
        return Items.FindIndex(x => x == item) != -1;
    }
    public int GetItemCount(AssetItem item)
    {
        int count = 0;

        foreach (AssetItem inventoryItem in Items)
        {
            if (item = inventoryItem)
                count++;
        }

        return count;
    }
    public void Reset()
    {
        Items = new List<AssetItem>();
        Coins = 0;
    }
}
