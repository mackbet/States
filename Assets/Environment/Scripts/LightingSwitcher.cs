using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.Events;

public class LightingSwitcher : MonoBehaviour
{
    [SerializeField] private PartOfDay[] SkyboxMaterials;
    [SerializeField] private int currentIndex;

    private Material skyboxMaterial;
    private ReflectionProbe baker;

    public static UnityEvent OnDay = new UnityEvent();
    public static UnityEvent OnNight = new UnityEvent();

    private void Start()
    {
        baker = gameObject.AddComponent<ReflectionProbe>();
        baker.cullingMask = 0;
        baker.refreshMode = ReflectionProbeRefreshMode.ViaScripting;
        baker.mode = ReflectionProbeMode.Realtime;
        baker.timeSlicingMode = ReflectionProbeTimeSlicingMode.NoTimeSlicing;
        RenderSettings.defaultReflectionMode = DefaultReflectionMode.Custom;

        skyboxMaterial = RenderSettings.skybox;

        SetPart(SkyboxMaterials[currentIndex]);
    }

    private void SetPart(PartOfDay part)
    {
        StopAllCoroutines();

        StartCoroutine(Delay(part.Duration));
        StartCoroutine(Lerp(part.LerpDuration));

        if (part.IsDay)
            OnDay.Invoke();
        else if (part.IsNight)
            OnNight.Invoke();
    }

    private IEnumerator Delay(float duration)
    {
        yield return new WaitForSeconds(duration);

        currentIndex = currentIndex + 1 >= SkyboxMaterials.Length ? 0 : currentIndex + 1;

        SetPart(SkyboxMaterials[currentIndex]);
    }
    private IEnumerator Lerp(float duration)
    {
        float time = 0;

        Color[] color = new Color[] { skyboxMaterial.GetColor("_SunDiscColor"), SkyboxMaterials[currentIndex].Material.GetColor("_SunDiscColor") };
        float[] multiplier = new float[] { skyboxMaterial.GetFloat("_SunDiscMultiplier"), SkyboxMaterials[currentIndex].Material.GetFloat("_SunDiscMultiplier") };
        float[] exponent = new float[] { skyboxMaterial.GetFloat("_SunDiscExponent"), SkyboxMaterials[currentIndex].Material.GetFloat("_SunDiscExponent") };
        
        Color[] haloColor = new Color[] { skyboxMaterial.GetColor("_SunHaloColor"), SkyboxMaterials[currentIndex].Material.GetColor("_SunHaloColor") };
        float[] haloMultiplier = new float[] { skyboxMaterial.GetFloat("_SunHaloExponent"), SkyboxMaterials[currentIndex].Material.GetFloat("_SunHaloExponent") };
        float[] haloContribution = new float[] { skyboxMaterial.GetFloat("_SunHaloContribution"), SkyboxMaterials[currentIndex].Material.GetFloat("_SunHaloContribution") };

        Color[] horizonColor = new Color[] { skyboxMaterial.GetColor("_HorizonLineColor"), SkyboxMaterials[currentIndex].Material.GetColor("_HorizonLineColor") };
        float[] horizonMultiplier = new float[] { skyboxMaterial.GetFloat("_HorizonLineExponent"), SkyboxMaterials[currentIndex].Material.GetFloat("_HorizonLineExponent") };
        float[] horizonContribution = new float[] { skyboxMaterial.GetFloat("_HorizonLineContribution"), SkyboxMaterials[currentIndex].Material.GetFloat("_HorizonLineContribution") };

        Color[] skyTop = new Color[] { skyboxMaterial.GetColor("_SkyGradientTop"), SkyboxMaterials[currentIndex].Material.GetColor("_SkyGradientTop") };
        Color[] skyBottom = new Color[] { skyboxMaterial.GetColor("_SkyGradientBottom"), SkyboxMaterials[currentIndex].Material.GetColor("_SkyGradientBottom") };
        float[] skyExponent = new float[] { skyboxMaterial.GetFloat("_SkyGradientExponent"), SkyboxMaterials[currentIndex].Material.GetFloat("_SkyGradientExponent") };


        while (time < 1)
        {
            skyboxMaterial.SetColor("_SunDiscColor", Color.Lerp(color[0], color[1], time));
            skyboxMaterial.SetFloat("_SunDiscMultiplier", Mathf.Lerp(multiplier[0], multiplier[1], time));
            skyboxMaterial.SetFloat("_SunDiscExponent", Mathf.Lerp(exponent[0], exponent[1], time));

            skyboxMaterial.SetColor("_SunHaloColor", Color.Lerp(haloColor[0], haloColor[1], time));
            skyboxMaterial.SetFloat("_SunHaloExponent", Mathf.Lerp(haloMultiplier[0], haloMultiplier[1], time));
            skyboxMaterial.SetFloat("_SunHaloContribution", Mathf.Lerp(haloContribution[0], haloContribution[1], time));

            skyboxMaterial.SetColor("_HorizonLineColor", Color.Lerp(horizonColor[0], horizonColor[1], time));
            skyboxMaterial.SetFloat("_HorizonLineExponent", Mathf.Lerp(horizonMultiplier[0], horizonMultiplier[1], time));
            skyboxMaterial.SetFloat("_HorizonLineContribution", Mathf.Lerp(horizonContribution[0], horizonContribution[1], time));

            skyboxMaterial.SetColor("_SkyGradientTop", Color.Lerp(skyTop[0], skyTop[1], time));
            skyboxMaterial.SetColor("_SkyGradientBottom", Color.Lerp(skyBottom[0], skyBottom[1], time));
            skyboxMaterial.SetFloat("_SkyGradientExponent", Mathf.Lerp(skyExponent[0], skyExponent[1], time));

            time += (Time.deltaTime / duration);
            yield return new WaitForEndOfFrame();
            UpdateEnvironment();
        }
    }
    private void UpdateEnvironment()
    {
        DynamicGI.UpdateEnvironment();
        baker.RenderProbe();

    }

    [Serializable]
    private class PartOfDay
    {
        [field: SerializeField] public Material Material { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public float LerpDuration { get; private set; }
        [field: SerializeField] public bool IsDay { get; private set; }
        [field: SerializeField] public bool IsNight { get; private set; }
    }


}
