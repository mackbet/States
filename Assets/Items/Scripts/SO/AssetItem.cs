using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AssetItem")]
public class AssetItem : ScriptableObject, IItem
{
    public string Name => _name;
    public Sprite UIIcon => _uiIcon;
    public Rarity Rarity => _rarity;

    [SerializeField] private string _name;
    [SerializeField] private Sprite _uiIcon;
    [SerializeField] private Rarity _rarity;
}
