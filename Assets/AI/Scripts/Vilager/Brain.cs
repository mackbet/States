using UnityEngine;

public class Brain : MonoBehaviour
{
    public static Brain Instance { get; private set; }

    [field: SerializeField] public AssetItem Wood { get; private set; }
    [field: SerializeField] public AIState GetWood { get; private set; }
    [field: SerializeField] public AIState SellWood { get; private set; }

    [field: SerializeField] public AssetItem Stone { get; private set; }
    [field: SerializeField] public AIState GetStone { get; private set; }
    [field: SerializeField] public AIState SellStone { get; private set; }


    [field: SerializeField] public AIState Patrol { get; private set; }


    private void Awake()
    {
        Instance = this;
    }

    public static AIState ComputeState(StateMachine stateMachine)
    {
        return Instance.Patrol;
        LootSpawner stone = AIResourceMap.GetClosestSpawner(stateMachine.transform.position, Instance.Stone);
        Market stoneMarket = stateMachine.Commonwealth.GetMarket(stateMachine.transform.position, Instance.Stone);
        float closestStone = 0;
        float closestStoneMarket = 0;
        if (stone)
            closestStone = Vector3.Distance(stateMachine.transform.position, stone.transform.position);
        if(stoneMarket)
            closestStoneMarket = Vector3.Distance(stateMachine.transform.position, stoneMarket.transform.position);

        LootSpawner wood = AIResourceMap.GetClosestSpawner(stateMachine.transform.position, Instance.Wood);
        Market woodMarket = stateMachine.Commonwealth.GetMarket(stateMachine.transform.position, Instance.Wood);
        float closestWood = 0;
        float closestWoodMarket = 0;
        if (wood)
            closestWood = Vector3.Distance(stateMachine.transform.position, wood.transform.position);
        if(woodMarket)
            closestWoodMarket = Vector3.Distance(stateMachine.transform.position, woodMarket.transform.position);


        if (stoneMarket && stoneMarket.CoinsEnough && (closestStoneMarket<closestStone || stateMachine.Inventory.IsFull) && stateMachine.Inventory.GetItemCount(Instance.Stone) > 0)
        {
            return Instance.SellStone;
        }
        else if (woodMarket && woodMarket.CoinsEnough && (closestWoodMarket < closestWood || stateMachine.Inventory.IsFull) && stateMachine.Inventory.GetItemCount(Instance.Wood) > 0)
        {
            return Instance.SellWood;
        }
        else
        {
            if(stone && wood)
            {
                return closestStone < closestWood ? Instance.GetStone : Instance.GetWood;
            }
            else
            {
                if (stone)
                    return Instance.GetStone;
                else if (wood)
                    return Instance.GetWood;
                else
                    return Instance.GetWood;
            }
        }
    }
}
