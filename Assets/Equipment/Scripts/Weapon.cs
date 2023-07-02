using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Weapon")]
public class Weapon : ScriptableObject
{
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public float AttackAngle { get; private set; }
    [field: SerializeField] public float AttackRecharge { get; private set; }
    [field: SerializeField] public float AttackTime { get; private set; }

    [field: SerializeField] public GameObject Model{ get; private set; }
}
