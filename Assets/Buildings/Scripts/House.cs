using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class House : Building
{
    [SerializeField] private StateMachine _character;
    [SerializeField] private float _delay;

    public UnityEvent<StateMachine> OnCharacterSpawned;

    private void Awake()
    {
        StartCoroutine(Delay());
    }

    public void SetCommonwealth(Commonwealth commonwealth, CityBuilder cityBuilder)
    {
        _character.Commonwealth = commonwealth;
        _character.CityBuilder = cityBuilder;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(_delay);

        _character.gameObject.SetActive(true);
        OnCharacterSpawned.Invoke(_character);
    }
}
