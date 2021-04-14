using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class PlayerData : ScriptableObject {
    [SerializeField] private double currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private double damageMultiplier;
    [SerializeField] private int soulOrbs;
    [SerializeField] private double defenseMultiplier;
    [SerializeField] private Sword activeSword;
    [SerializeField] private int burnTicks = 3;
    [SerializeField] private float slowModifier = 0.5f;

    public double CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public double DamageMultiplier { get => damageMultiplier; set => damageMultiplier = value; }
    public int SoulOrbs { get => soulOrbs; set => soulOrbs = value; }
    public double DefenseMultiplier { get => defenseMultiplier; set => defenseMultiplier = value; }
    public Sword ActiveSword { get => activeSword; set => activeSword = value; }
    public int BurnTicks { get => burnTicks; set => burnTicks = value; }
    public float SlowModifier { get => slowModifier; set => slowModifier = value; }

    public void AddSoulOrbs(int amount) {
        this.SoulOrbs += amount;
    }
}
