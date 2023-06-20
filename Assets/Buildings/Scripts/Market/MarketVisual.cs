using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MarketVisual : MonoBehaviour
{
    [SerializeField] private MarketSelection _marketSelection;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _sellPrice;
    [SerializeField] private TextMeshProUGUI _buyPrice;
    [SerializeField] private Image _buyImage;
    [SerializeField] private Image _sellImage;

    private void OnEnable()
    {
        UpdateMarket(_marketSelection.Market);
        _marketSelection.OnMarketSet += UpdateMarket;
    }
    private void OnDisable()
    {
        _marketSelection.OnMarketSet -= UpdateMarket;
    }

    private void UpdateMarket(Market market)
    {
        _title.text = market.Name;
        _sellPrice.text = market.BuyPrice.ToString();
        _buyPrice.text = market.SellPrice.ToString();

        _buyImage.sprite = market.Item.UIIcon;
        _sellImage.sprite = market.Item.UIIcon;
    }
}
