using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AI States/SellResource")]
public class SellResource : AIComplexState
{
    [SerializeField] AIState _goState;
    [SerializeField] AIState _sellState;
    public override void Initialize(StateMachine aiCharacter)
    {
        base.Initialize(aiCharacter);

        SetState(_goState, Sell);
    }

    private void Sell()
    {
        SetState(_sellState, FinishState);
    }
}
