using UnityEngine;
using UnityEngine.Events;

public class TriggerEventHandler : MonoBehaviour
{
    public UnityEvent<Collider> onTrigger;

    private void OnTriggerEnter(Collider other)
    {
        onTrigger.Invoke(other);
    }
}
