using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private AttackController _attackController;
    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        _attackController.onAttackStarted += SetAttack;
        _attackController.onAttackDelayed += StopAttack;
    }
    private void OnDisable()
    {
        _attackController.onAttackStarted -= SetAttack;
        _attackController.onAttackDelayed -= StopAttack;
    }
    public void SetSpeed(float speed)
    {
        ResetPosition();
        _animator.SetFloat("speed", speed);
    }

    public void SetAttack()
    {
        ResetPosition();
        _animator.SetBool("attack", true);
    }
    public void StopAttack()
    {
        ResetPosition();
        _animator.SetBool("attack", false);
    }

    private void ResetPosition()
    {
        _animator.transform.localPosition = Vector3.zero;
    }
}
