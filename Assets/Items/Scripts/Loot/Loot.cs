using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Loot : MonoBehaviour
{
    [field:SerializeField] public AssetItem Item { get; private set; }
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private LootVisual _lootVisual;
    [SerializeField] private float _lifetime;

    public UnityEvent<Loot> OnTaken;

    private void Start()
    {
        StartCoroutine(Die());
    }
    public void SetItem(AssetItem item)
    {
        Item = item;
        _lootVisual.Initialize(Item);
    }

    public void Toss(float power)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere;
        randomDirection.y = Mathf.Abs(randomDirection.y);
        randomDirection.Normalize();

        _rigidbody.velocity = randomDirection * power;
    }

    public void Taken()
    {
        OnTaken.Invoke(this);
        Destroy(gameObject);
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(_lifetime);

        Taken();
    }
}
