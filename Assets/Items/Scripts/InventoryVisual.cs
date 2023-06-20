using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class InventoryVisual : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private InventoryCell _inventoryCellPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private TextMeshProUGUI _textCoinsCount;

    [SerializeField] private AssetItem[] _validItems;
    
    public void SetValidItems(List<AssetItem> newList)
    {
        _validItems = newList.ToArray();
    }
    
    private void OnEnable()
    {
        UpdateItems(_inventory.Items);

        _inventory.OnItemsUpated += UpdateItems;
    }
    private void OnDisable()
    {
        _inventory.OnItemsUpated -= UpdateItems;
    }
    private void UpdateItems(List<AssetItem> items)
    {
        foreach (Transform child in _container)
            Destroy(child.gameObject);


        items.ForEach(item =>
        {
            bool isValid = _validItems.Length > 0 ? _validItems.ToList().FindIndex(x => x == item) >= 0 : true;

            if (isValid)
            {
                InventoryCell cell = Instantiate(_inventoryCellPrefab, _container);
                cell.Initialize(transform);
                cell.Render(item);
            }
        });

        _textCoinsCount.text = _inventory.Coins.ToString();
    }
}
