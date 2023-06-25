
public interface IMarket
{
    public string Name { get; }
    public Commonwealth Ñommonwealth { get; }
    public AssetItem Item { get; }
    public int SellPrice{ get; }
    public int BuyPrice { get; }
    public int ItemCount { get; }
    public void Buy();
    public void Sell();
}
