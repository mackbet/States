using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AIMovement : MonoBehaviour, IMovable, IRotatable
{
    public string state;
    [SerializeField] private float _chaseAccurancy;
    [SerializeField] private float _chaseFrequency;
    [SerializeField] private NavMeshAgent _agent;
    [field: SerializeField] public UnityEvent<float> OnMoved { get; set; }
    [field: SerializeField] public UnityEvent<Vector3> OnRotated { get; set; }

    public UnityEvent OnChased;

    private Coroutine _chaseCoroutine;

    [SerializeField] private Transform test;

    public void MoveTo(Vector3 position, string StateName)
    {
        if (_chaseCoroutine != null)
        {
            StopCoroutine(_chaseCoroutine);
            _chaseCoroutine = null;
        }

        _agent.isStopped = false;
        _agent.SetDestination(position);
        test.position = position;
        OnMoved.Invoke(_agent.speed);
        state = "MoveTo " + StateName;
    }

    public void StartÑhase(Transform target)
    {
        if (_chaseCoroutine != null)
        {
            StopCoroutine(_chaseCoroutine);
            _chaseCoroutine = null;
        }

        _agent.isStopped = false;
        _chaseCoroutine = StartCoroutine(Chase(target));
        OnMoved.Invoke(_agent.speed);
        state = "StartÑhase " + target.name;
    }

    private IEnumerator Chase(Transform target)
    {
        while (target != null && Vector3.Distance(transform.position,target.position)> _chaseAccurancy)
        {
            _agent.SetDestination(target.position);

            test.position = target.position;

            yield return new WaitForSeconds(1 / _chaseFrequency);
        }
        StopMoving("Chased");
        OnChased.Invoke();
    }

    public void StopMoving(string tag)
    {
        if (_chaseCoroutine != null)
        {
            StopCoroutine(_chaseCoroutine);
            _chaseCoroutine = null;
        }

        _agent.isStopped = true;
        OnMoved.Invoke(0);
        state = "StopMoving " + tag;
    }
}
