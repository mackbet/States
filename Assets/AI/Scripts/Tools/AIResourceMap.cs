using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIResourceMap : MonoBehaviour
{
    public static AIResourceMap Instance;

    private static List<LootSpawner> spawners = new List<LootSpawner>();

    private void Awake()
    {
        Instance = this;
    }
    public static void Register(LootSpawner spawner)
    {
        spawners.Add(spawner);
        spawner.HealthObject.OnDead += ()=> RemoveSpawner(spawner);
        spawner.isRegistered = true;
    }
    public static LootSpawner GetClosestSpawner(Vector3 position, AssetItem item)
    {
        float? minDistance = null;
        LootSpawner selectedSpawner = null;

        foreach (LootSpawner spawner in spawners)
        {
            if (!spawner.HealthObject.IsAlive)
                RemoveSpawner(spawner);
            else if(spawner.HasItem(item))
            {
                float currentDistance = Vector3.Distance(position, spawner.transform.position);

                if (minDistance == null || minDistance > currentDistance)
                {
                    minDistance = currentDistance;
                    selectedSpawner = spawner;
                }
            }
        }
        return selectedSpawner;
    }
    private static void RemoveSpawner(LootSpawner spawner)
    {
        spawners.Remove(spawner);
    }
}
