using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatePanel : MonoBehaviour
{
    [SerializeField] private StateMachine _stateMachine;
    [SerializeField] private Image _image;

    private void OnEnable()
    {
        _stateMachine.OnStateChanged += SetSprite;
    }

    private void OnDisable()
    {
        _stateMachine.OnStateChanged -= SetSprite;
    }

    private void SetSprite(AIState state)
    {
        _image.sprite = state.UISprite;
    }
}
