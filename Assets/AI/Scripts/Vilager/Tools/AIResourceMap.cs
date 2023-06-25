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
    public void FillSpawnersManualy(GameObject newEnv)
    {
        LootSpawner[] components = newEnv.transform.GetComponentsInChildren<LootSpawner>();
        spawners.AddRange(components);
    }

    public static bool IsEmpty(Vector3 position)
    {
        int count = 0;
        foreach(LootSpawner spawner in spawners.ToArray())
        {
            if (!spawner)
                RemoveSpawner(spawner);
            else
                count++;
        }
        return count == 0;
    }
    public static void Register(LootSpawner spawner)
    {
        spawners.Add(spawner);
        spawner.HealthObject.OnDead += ()=> RemoveSpawner(spawner);
        spawner.isRegistered = true;
    }
    public static int GetLootSpawnerCount(AssetItem item)
    {
        int count = 0;

        foreach (LootSpawner spawner in spawners)
        {
            if (spawner && spawner.HasItem(item))
                count++;
        }

        return count;
    }
    public static LootSpawner GetClosestSpawner(Vector3 position, AssetItem item)
    {
        float? minDistance = null;
        LootSpawner selectedSpawner = null;

        foreach (LootSpawner spawner in spawners.ToArray())
        {
            if (!spawner || !spawner.HealthObject.IsAlive)
            {
                RemoveSpawner(spawner);
            }
            else if (spawner.HasItem(item))
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
