using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : HealthObject
{
    [SerializeField] GameObject pebbles;

    protected override void Die()
    {
        pebbles.transform.SetParent(null, true);
        pebbles.gameObject.SetActive(true);

        base.Die();
    }
}
