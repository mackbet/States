using UnityEngine;
using UnityEngine.Events;

public class CollideEventHandler : MonoBehaviour
{
    public UnityEvent<Collision> onCollide;

    private void OnCollisionEnter(Collision collision)
    {
        onCollide.Invoke(collision);
    }
}
