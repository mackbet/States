using System.Collections.Generic;
using UnityEngine;

public class PanoplyVisual : MonoBehaviour
{
    [SerializeField] private Vilager _vilager;
    [SerializeField] private List<PanoplyData> _panoplyModels;

    private GameObject _lastPanoply;

    private void Start()
    {
        foreach (PanoplyData panoplyModel in _panoplyModels)
        {
            SkinnedMeshRenderer[] skinnedMeshRenderers = panoplyModel.Model.GetComponentsInChildren<SkinnedMeshRenderer>();

            foreach (SkinnedMeshRenderer skinnedMeshRenderer in skinnedMeshRenderers)
            {
                skinnedMeshRenderer.material = _vilager.Parameters.PanoplyMaterial;
            }
        }
    }

    private void OnEnable()
    {
        UpdateVisual(_vilager.Panoply);
        _vilager.OnPanoplyChanged += UpdateVisual;
    }

    private void OnDisable()
    {
        _vilager.OnPanoplyChanged -= UpdateVisual;
    }

    private void UpdateVisual(Panoply panoply)
    {
        if (_lastPanoply)
            _lastPanoply.SetActive(false);

        int index = _panoplyModels.FindIndex((x) => x.Panoply == panoply);

        if (index == -1)
            return;

        _lastPanoply = _panoplyModels[index].Model;
        _lastPanoply.SetActive(true);
    }



    [System.Serializable]
    private class PanoplyData
    {
        [field:SerializeField] public Panoply Panoply { get; private set; }
        [field: SerializeField] public GameObject Model { get; private set; }
    }
}
