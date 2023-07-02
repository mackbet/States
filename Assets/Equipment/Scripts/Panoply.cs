using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Panoply")]
public class Panoply : ScriptableObject
{
    [field: SerializeField] public int Armor { get; private set; }
}
