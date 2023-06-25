using UnityEngine;
using UnityEngine.Events;

public abstract class Market : MonoBehaviour, IMarket
{
    public string Name => _name;
    public AssetItem Item => _item;
    public Commonwealth Ñommonwealth => _commonwealth;
    public int SellPrice => Ñommonwealth.GetItemSellPrice(Item);
    public int BuyPrice => Ñommonwealth.GetItemBuyPrice(Item);
    public int ItemCount => Ñommonwealth.GetItemCount(Item);
    public bool CoinsEnough => Ñommonwealth.CoinsCount > BuyPrice;
    public bool ItemEnough => Ñommonwealth.GetItemCount(Item) > 0;



    [HideInInspector] public UnityEvent<AssetItem, int> OnItemSold;
    [HideInInspector] public UnityEvent<AssetItem, int> OnItemBought;

    [SerializeField] private string _name;
    [SerializeField] private AssetItem _item;
    [SerializeField] private Commonwealth _commonwealth;

    public void Buy()
    {
        OnItemBought.Invoke(Item, BuyPrice);
    }
    public void Sell()
    {
        OnItemSold.Invoke(Item, SellPrice);
    }
}
