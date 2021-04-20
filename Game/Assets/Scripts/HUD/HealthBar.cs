using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour {
    public Slider slider;
    public Health health;
    public TextMeshProUGUI hpText;

    private Vector3 healthBarLocalScale;

    private void Start() {
        InitialiseHealthBar();

        health.setMaxHealth.AddListener(SetMaxHealth);
        health.setCurrentHealth.AddListener(SetCurrentHealth);
        health.sliderChange.AddListener(Appear);
        health.flipHealthBarRight.AddListener(FlipRight);
        health.flipHealthBarLeft.AddListener(FlipLeft);

        healthBarLocalScale = transform.localScale;
    }

    private void OnDestroy() {
        health.setMaxHealth.RemoveListener(SetMaxHealth);
        health.setCurrentHealth.RemoveListener(SetCurrentHealth);
        health.sliderChange.RemoveListener(Appear);
        health.flipHealthBarRight.RemoveListener(FlipRight);
        health.flipHealthBarLeft.RemoveListener(FlipLeft);
    }

    private void InitialiseHealthBar() {
        SetMaxHealth();
        slider.value = slider.maxValue;
    }

    private void SetMaxHealth() {
        slider.maxValue = (float)health.maxHealth;
    }

    private void SetCurrentHealth() {
        slider.value = (float)health.currentHealth;
        hpText.text = health.currentHealth + " / " + health.maxHealth;
    }

    private void Appear(bool appear)
    {
        slider.gameObject.SetActive(appear);
    }

    private void FlipRight() {
        transform.localScale = new Vector3(
            healthBarLocalScale.x,
            healthBarLocalScale.y,
            healthBarLocalScale.z);
    }

    private void FlipLeft() {
        transform.localScale = new Vector3(
            -healthBarLocalScale.x,
            healthBarLocalScale.y,
            healthBarLocalScale.z);
    }
}
