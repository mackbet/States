using System;
using UnityEngine;

public class MarketScanner : MonoBehaviour
{
    [field:SerializeField] public Market TargetMarket { get; private set; }

    public Action<Market> OnMarketSelected;
    public Action OnMarketDeselected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Market market))
        {
            TargetMarket = market;
            OnMarketSelected?.Invoke(TargetMarket);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Market market))
        {
            if (TargetMarket == market)
            {
                TargetMarket = null;
                OnMarketDeselected?.Invoke();
            }
        }
    }
}
