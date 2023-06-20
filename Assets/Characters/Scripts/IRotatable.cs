using UnityEngine;
using UnityEngine.Events;

public interface IRotatable
{
    public UnityEvent<Vector3> OnRotated { get; set; }
}
