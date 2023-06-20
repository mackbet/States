using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Market : MonoBehaviour, IMarket
{
    public string Name => _name;
    public AssetItem Item => _item;
    public int SellPrice => _sellPrice;
    public int BuyPrice => _buyPrice;
    public int ItemCount => _itemCount;
    public int CoinsCount => _coinsCount;

    public bool CoinsEnough => _coinsCount > _buyPrice;
    public bool ItemEnough => _itemCount > 0;


    [SerializeField] private string _name;
    [SerializeField] private AssetItem _item;
    [SerializeField] private int _sellPrice;
    [SerializeField] private int _buyPrice;
    [SerializeField] private int _itemCount;
    [SerializeField] private int _coinsCount;

    public void Buy()
    {
        _itemCount++;
        _coinsCount -= BuyPrice;
    }

    public void Sell()
    {
        _itemCount--;
        _coinsCount += SellPrice;
    }
}
