using UnityEngine;
using UnityEngine.Events;

public abstract class Market : MonoBehaviour, IMarket
{
    public string Name => _name;
    public AssetItem Item => _item;
    public �ommonwealth �ommonwealth => _commonwealth;
    public int SellPrice => �ommonwealth.GetItemSellPrice(Item);
    public int BuyPrice => �ommonwealth.GetItemBuyPrice(Item);
    public int ItemCount => �ommonwealth.GetItemCount(Item);
    public bool CoinsEnough => �ommonwealth.CoinsCount > BuyPrice;
    public bool ItemEnough => �ommonwealth.GetItemCount(Item) > 0;



    [HideInInspector] public UnityEvent<AssetItem, int> OnItemSold;
    [HideInInspector] public UnityEvent<AssetItem, int> OnItemBought;

    [SerializeField] private string _name;
    [SerializeField] private AssetItem _item;
    [SerializeField] private �ommonwealth _commonwealth;

    public void Buy()
    {
        OnItemBought.Invoke(Item, BuyPrice);
    }
    public void Sell()
    {
        OnItemSold.Invoke(Item, SellPrice);
    }
}
