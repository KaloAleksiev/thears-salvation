using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SliderChangeEvent : UnityEvent<bool> { }
public class Health : MonoBehaviour {
    public int maxHealth;
    public double currentHealth;
    public UnityEvent setMaxHealth;
    public UnityEvent setCurrentHealth;
    public SliderChangeEvent sliderChange;
    public Animator animator;

    private void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(double damage) {

        animator.SetTrigger("Hurt");
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        setCurrentHealth.Invoke();
    }
}
