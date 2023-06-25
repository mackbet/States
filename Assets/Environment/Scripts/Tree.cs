using UnityEngine;

public class Tree : HealthObject
{
    public bool isRegistered;


    [SerializeField] GameObject stump;
    protected override void Die()
    {
        if (!stump)
            return;

        stump.transform.SetParent(transform.parent, true);
        stump.gameObject.SetActive(true);

        base.Die();
    }
}
