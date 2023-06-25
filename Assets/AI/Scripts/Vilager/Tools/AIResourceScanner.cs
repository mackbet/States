using System.Collections;
using UnityEngine;

public class AIResourceScanner : MonoBehaviour
{
    [SerializeField] private float viewDistance;
    private void Awake()
    {
        StartCoroutine(AIReresourceScanner());
    }

    IEnumerator AIReresourceScanner()
    {
        while (true)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, viewDistance);

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out LootSpawner lootSpawner))
                {
                    if (!lootSpawner.isRegistered)
                        AIResourceMap.Register(lootSpawner);
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
