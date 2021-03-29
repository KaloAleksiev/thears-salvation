using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Slider slider;
    public Health health;

    private void Start() {
        SetMaxHealth(health.maxHealth);
        health.healthChange.AddListener(SetHealth);
    }

    private void OnDestroy() {
        health.healthChange.RemoveListener(SetHealth);
    }

    private void SetMaxHealth(int health) {
        slider.maxValue = health;
        slider.value = health;
    }

    private void SetHealth() {
        slider.value = health.currentHealth;
    }
}
