using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Slider slider;
    public Health health;

    private void Start() {
        InitialiseHealthBar();

        health.setMaxHealth.AddListener(SetMaxHealth);
        health.setCurrentHealth.AddListener(SetCurrentHealth);
        health.sliderChange.AddListener(Appear);
    }

    private void OnDestroy() {
        health.setMaxHealth.RemoveListener(SetMaxHealth);
        health.setCurrentHealth.RemoveListener(SetCurrentHealth);
        health.sliderChange.RemoveListener(Appear);
    }

    private void InitialiseHealthBar() {
        SetMaxHealth();
        slider.value = slider.maxValue;
    }

    private void SetMaxHealth() {
        slider.maxValue = health.maxHealth;
    }

    private void SetCurrentHealth() {
        slider.value = (float)health.currentHealth;
    }

    private void Appear(bool appear)
    {
        slider.gameObject.SetActive(appear);
    }
}
