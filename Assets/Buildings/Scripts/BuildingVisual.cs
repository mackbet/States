using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingVisual : MonoBehaviour
{
    public UnityEvent OnDay;
    public UnityEvent OnNight;

    private void Awake()
    {
        LightingSwitcher.OnDay.AddListener(() => OnDay.Invoke());
        LightingSwitcher.OnNight.AddListener(() => OnNight.Invoke());
    }
}
