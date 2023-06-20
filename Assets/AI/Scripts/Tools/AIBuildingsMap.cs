using System.Collections.Generic;
using UnityEngine;

public static class AIBuildingsMap
{
    private static List<Market> markets = new List<Market>();

    public static void AddMarket(Market market)
    {
        markets.Add(market);
    }
    public static Market GetMarket(AssetItem item, Vector3 position)
    {
        float? minDistance = null;
        Market selectedMarket = null;
        foreach (Market market in markets)
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
    private static void RemoveMarket(Market market)
    {
        markets.Remove(market);
    }
}
