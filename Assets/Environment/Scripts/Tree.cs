using UnityEngine;

public class Tree : HealthObject
{
    public bool isRegistered;


    [SerializeField] GameObject stump;
    protected override void Die()
    {
        stump.transform.SetParent(null, true);
        stump.gameObject.SetActive(true);

        base.Die();
    }
}
