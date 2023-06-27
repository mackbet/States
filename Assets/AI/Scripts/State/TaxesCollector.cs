using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxesCollector : MonoBehaviour
{
    public List<House> BuiltHouses => _builtHouses;

    [SerializeField] private Commonwealth _ñommonwealth;

    [SerializeField] private List<House> _builtHouses;
    [SerializeField] private List<StateMachine> _characters;

    [SerializeField] private float _period;
    [SerializeField] private int _taxValue;

    private void Awake()
    {
        Initialize();
        StartCoroutine(CollectTaxes());
    }

    public void AddHouse(House house)
    {
        _builtHouses.Add(house);
        house.OnCharacterSpawned.AddListener((value) => _characters.Add(value));
        house.OnDead += () => RemoveHouse(house);
    }
    public void RemoveHouse(House house)
    {
        _builtHouses.Remove(house);
    }
    private void Initialize()
    {
        foreach (House house in _builtHouses)
        {
            house.OnCharacterSpawned.AddListener((value) => _characters.Add(value));
        }
    }
    IEnumerator CollectTaxes()
    {
        while (true)
        {
            yield return new WaitForSeconds(_period);

            foreach (StateMachine character in _characters)
            {
                if (character.Inventory.CoinsEnough(_taxValue))
                {
                    character.Inventory.PayTaxes(_taxValue);
                    _ñommonwealth.GetTax(_taxValue);
                }
            }
        }
    }
}
