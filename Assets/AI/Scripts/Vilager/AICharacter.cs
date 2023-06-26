using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class AICharacter : Agent
{
    [SerializeField] StateMachine _stateMachine;

    [SerializeField] List<AssetItem> assetItemTypes;

    [SerializeField] private GameObject envirovment;
    [SerializeField] private GameObject lastEnv;
    private Vector3 initialPos;

    private void Awake()
    {
        _stateMachine.Inventory.OnItemGot.AddListener(CustomAddReward);
        _stateMachine.Inventory.OnCoinGot.AddListener((value) => CustomAddReward(RewardValues.COIN_REWARD + value));
        _stateMachine.OnStateFinished.AddListener(NextDesition);

        initialPos = _stateMachine.transform.position;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        foreach (AssetItem assetItem in assetItemTypes)
        {
            List<float> itemData;
            int CountInInventory = _stateMachine.Inventory.GetItemCount(assetItem);

            int Price = _stateMachine.Commonwealth.GetItemBuyPrice(assetItem);

            float MarketDistance = 30f;
            Market market = _stateMachine.Commonwealth.GetMarket(assetItem, _stateMachine.transform.position);
            if (market)
                MarketDistance = Vector3.Distance(market.transform.position, _stateMachine.transform.position);

            int Exist = AIResourceMap.GetLootSpawnerCount(assetItem);

            float ResourcesDistance = 1000f;
            LootSpawner lootSpawner = AIResourceMap.GetClosestSpawner(_stateMachine.transform.position, assetItem);
            if(lootSpawner)
                ResourcesDistance = Vector3.Distance(lootSpawner.transform.position, _stateMachine.transform.position);

            itemData = new List<float> { CountInInventory, Price, MarketDistance, Exist, ResourcesDistance };

            sensor.AddObservation(itemData);
        }
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        CustomAddReward(RewardValues.STATE_PENALTY);
        int index = actions.DiscreteActions[0];

        _stateMachine.SetState(Brain.GetState(index));
    }
    public void NextDesition(AIState lastState)
    {
        RequestDecision();
    }
    public override void OnEpisodeBegin()
    {
        StopAllCoroutines();

        _stateMachine.transform.position = initialPos;
        _stateMachine.Inventory.Reset();

        StartCoroutine(TimePenalty());
        
        NextDesition(null);
    }
    private void End()
    {
        EndEpisode();
    }
    private void CustomAddReward(float value)
    {
        AddReward(value);
    }
    private void CustomAddReward(AssetItem item)
    {
        AddReward(RewardValues.RESOURCE_REWARD);
    }

    private IEnumerator TimePenalty()
    {
        while (true)
        {
            CustomAddReward(RewardValues.TIME_PENALTY);
            yield return null;
        }
    }
}
