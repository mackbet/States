using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private List<BindAction> actions = new List<BindAction>();

    public UnityEvent<Vector3> movementInput;

    void Update()
    {
        foreach (BindAction action in actions)
        {
            if (Input.GetKey(action.code))
                action.onClicked.Invoke();
        }

        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * horizontalInput + transform.forward * verticalInput;
        moveDirection.Normalize();

        movementInput.Invoke(moveDirection);


    }

    [Serializable]
    private class BindAction
    {
        [field: SerializeField]
        public KeyCode code { get; private set; }
        public UnityEvent onClicked;
    }
}
