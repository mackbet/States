using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : HealthObject
{
    [SerializeField] GameObject pebbles;

    protected override void Die()
    {
        if (!pebbles)
            return;

        pebbles.transform.SetParent(transform.parent, true);
        pebbles.gameObject.SetActive(true);

        base.Die();
    }
}
