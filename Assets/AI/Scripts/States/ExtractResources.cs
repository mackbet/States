using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AI States/ExtractResources")]
public class ExtractResources : AIComplexState
{
    [SerializeField] AIState _goState;
    [SerializeField] AIState _crushState;
    [SerializeField] AIState _collectState;
    public override void Initialize(StateMachine aiCharacter)
    {
        base.Initialize(aiCharacter);

        SetState(_goState, GetResource);
    }

    public void GetResource()
    {
        SetState(_crushState, CollectLoot);
    }

    public void CollectLoot()
    {
        SetState(_collectState, FinishState);
    }


}
