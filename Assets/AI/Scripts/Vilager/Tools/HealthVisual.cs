using UnityEngine;
using UnityEngine.UI;

public class HealthVisual : MonoBehaviour
{
    [SerializeField] private HealthObject healthObject;
    [SerializeField] private Image fillField;
    private float maxHealth;

    private void Start()
    {
        maxHealth = healthObject.Health;
    }

    private void OnEnable()
    {
        healthObject.OnHealthChanged += UpdateVisual;
    }

    private void OnDisable()
    {
        healthObject.OnHealthChanged -= UpdateVisual;
    }

    private void UpdateVisual(float health)
    {
        if (health > maxHealth)
            maxHealth = health;

        fillField.fillAmount = health / maxHealth;
    }
}
