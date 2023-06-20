using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Weapon")]
public class Weapon : ScriptableObject
{
    [field: SerializeField]
    public int damage { get; private set; }

    [field: SerializeField]
    public float attackRange { get; private set; }

    [field: SerializeField]
    public float attackAngle { get; private set; }

    [field: SerializeField]
    public float attackRecharge { get; private set; }

    [field: SerializeField]
    public float attackTime { get; private set; }
}
