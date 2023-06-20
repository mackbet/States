using UnityEngine;

public class CharacterRotator : MonoBehaviour
{
    [SerializeField]
    private Transform _model;

    [SerializeField]
    private float speed;

    public void Rotate(Vector3 moveDirection)
    {
        if (moveDirection == Vector3.zero)
            return;

        moveDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        _model.rotation = Quaternion.RotateTowards(_model.rotation, targetRotation, speed);
    }
    public void RotateInstantly(Vector3 direction)
    {
        if (direction == Vector3.zero)
            return;

        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        _model.rotation = targetRotation;
    }
}