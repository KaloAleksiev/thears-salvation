using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SliderChangeEvent : UnityEvent<bool> { }
public class Health : MonoBehaviour {
    public int maxHealth;
    public int currentHealth;
    public UnityEvent healthChange;
    public SliderChangeEvent sliderChange;
    public Animator animator;

    private void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        animator.SetTrigger("Hurt");
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        healthChange.Invoke();
    }
}
