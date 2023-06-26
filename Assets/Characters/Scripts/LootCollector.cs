using UnityEngine;

public class LootCollector : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out Loot loot))
        {
            _inventory.AddItem(loot.Item);

            loot.Taken();
        }
    }
}
