using UnityEngine;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class SliderChangeEvent : UnityEvent<bool> { }
public class Health : MonoBehaviour {
    public int maxHealth;
    public double currentHealth;
    public UnityEvent setMaxHealth;
    public UnityEvent setCurrentHealth;
    public SliderChangeEvent sliderChange;
    public Animator unitAnimator;
    public Animator receivedDamageAnimator;
    public TextMeshProUGUI receivedDamageText;

    private void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(double damage) {
        currentHealth -= damage;
        unitAnimator.SetTrigger("Hurt");
        receivedDamageText.text = "-" + damage.ToString();
        receivedDamageAnimator.SetTrigger("ReceiveDamageUI");
        if (currentHealth < 0) currentHealth = 0;
        setCurrentHealth.Invoke();
    }
}
