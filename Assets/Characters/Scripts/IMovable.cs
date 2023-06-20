using UnityEngine.Events;

public interface IMovable
{
    public UnityEvent<float> OnMoved { get; set; }
}
