using System;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    public bool isRegistered;
    [field: SerializeField] public HealthObject HealthObject { get; private set; }
    [SerializeField] private Loot _lootPrefab;
    [SerializeField] private List<SpawnableLoot> lootList;

    [SerializeField] private float _power;


    public bool HasItem(AssetItem item)
    {
        foreach (SpawnableLoot loot in lootList)
        {
            if (loot.Item == item)
                return true;
        }

        return false;
    }

    private void OnEnable()
    {
        HealthObject.OnDead += Spawn;
    }

    private void OnDisable()
    {
        HealthObject.OnDead -= Spawn;
    }
    private void Spawn()
    {
        lootList.ForEach(spawnableLoot=>
        {
            int randomInt = UnityEngine.Random.Range(0, 100);
            if (randomInt< spawnableLoot.Percentage)
            {
                Loot loot = Instantiate(_lootPrefab, transform.position, Quaternion.identity);
                loot.SetItem(spawnableLoot.Item);
                loot.Toss(_power);
            }

        });
    }

    [System.Serializable]
    private class SpawnableLoot
    {
        [field: SerializeField] public AssetItem Item { get; private set; }
        [Range(0, 100)]
        public float Percentage;
    }
}
