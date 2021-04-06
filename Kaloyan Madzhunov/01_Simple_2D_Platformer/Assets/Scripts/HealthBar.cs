using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Slider slider;
    public Health health;

    private void Start() {
        SetMaxHealth(health.maxHealth);
        health.healthChange.AddListener(SetHealth);
        health.sliderChange.AddListener(Appear);
    }

    private void OnDestroy() {
        health.healthChange.RemoveListener(SetHealth);
        health.sliderChange.RemoveListener(Appear);
    }

    private void SetMaxHealth(int health) {
        slider.maxValue = health;
        slider.value = health;
    }

    private void SetHealth() {
        slider.value = health.currentHealth;
    }

    private void Appear(bool appear)
    {
        slider.gameObject.SetActive(appear);
    }
}
