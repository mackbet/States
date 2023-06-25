using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AIParameters")]
public class AIParameters : ScriptableObject
{
    [field: SerializeField] public int TeamId { get; private set; }
}
