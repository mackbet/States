using System;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMovement : MonoBehaviour, IMovable, IRotatable
{
    [SerializeField]
    private CharacterController _controller;
    [SerializeField]
    private AttackController _attackController;

    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;

    [field: SerializeField] public UnityEvent<float> OnMoved { get; set; }
    [field: SerializeField] public UnityEvent<Vector3> OnRotated { get; set; }

    private bool movementDisabled;

    public void Move(Vector3 moveDirection)
    {
        bool isGrounded = _controller.isGrounded;

        

        if (movementDisabled)
        {
            _controller.Move(Vector3.zero);

            OnMoved.Invoke(_controller.velocity.magnitude);
            OnRotated.Invoke(_controller.velocity.normalized);

            return;
        }


        moveDirection = moveDirection * moveSpeed * Time.deltaTime;
        if (isGrounded && moveDirection.y < 0)
            moveDirection.y = 0f;
        else
            moveDirection.y += gravity * Time.deltaTime;

        if (moveDirection.magnitude > 0)
        {

            _controller.Move(moveDirection);

            OnMoved.Invoke(_controller.velocity.magnitude);
            OnRotated.Invoke(_controller.velocity.normalized);
        }
        else if (_controller.velocity.magnitude != 0)
        {
            _controller.Move(Vector3.zero);

            OnMoved.Invoke(_controller.velocity.magnitude);
            OnRotated.Invoke(_controller.velocity.normalized);
        }
    }

    private void OnEnable()
    {
        _attackController.onAttackStarted += DisabledMovement;
        _attackController.onAttackDelayed += EnabledMovement;
    }
    private void OnDisable()
    {
        _attackController.onAttackStarted -= DisabledMovement;
        _attackController.onAttackDelayed -= EnabledMovement;
    }
    private void EnabledMovement()
    {
        movementDisabled = false;
    }
    private void DisabledMovement()
    {
        movementDisabled = true;
    }
}
