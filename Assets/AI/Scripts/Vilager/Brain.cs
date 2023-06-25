using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    public static Brain Instance { get; private set; }

    public AIState[] PossibleStates;

    private void Awake()
    {
        Instance = this;
    }

    public static AIState GetState(int index)
    {
        return Instance.PossibleStates[index];
    }

    public static AIState ComputeState(AIParameters parameters)
    {
        return Instance.PossibleStates[Random.Range(0, Instance.PossibleStates.Length)];
    }
}
