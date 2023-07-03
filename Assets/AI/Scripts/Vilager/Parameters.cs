using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AIParameters")]
public class Parameters : ScriptableObject
{
    [field: SerializeField] public int TeamId { get; private set; }

    [field: SerializeField] public Material BuildingMaterial { get; private set; }
    [field: SerializeField] public Material PanoplyMaterial { get; private set; }

}

public enum Specialization
{
    Worker,
    Soldier
}
