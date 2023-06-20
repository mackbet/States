using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsMapInitializator : MonoBehaviour
{
    [SerializeField] private List<Market> initMarketList;
    private void Awake()
    {
        foreach(Market market in initMarketList)
        {
            AIBuildingsMap.AddMarket(market);
        }
    }
}
