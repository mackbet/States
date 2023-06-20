using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMarket
{
    public string Name { get; }
    public AssetItem Item { get; }
    public int SellPrice{ get; }
    public int BuyPrice { get; }
    public int ItemCount { get; }
    public int CoinsCount{ get; }
    public void Buy();
    public void Sell();
}
