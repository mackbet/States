using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootVisual : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private LootRarity lootRarity;

    public void Initialize(AssetItem item)
    {
        SetRarity(item.Rarity);
        SetSprite(item.UIIcon);
    }

    private void SetRarity(Rarity rarity)
    {
        _meshRenderer.material.SetColor("_Color", lootRarity.GetColor(rarity));
    }
    private void SetSprite(Sprite sprite)
    {
        _particleSystem.GetComponent<ParticleSystemRenderer>().material.mainTexture = sprite.texture;
    }
}
