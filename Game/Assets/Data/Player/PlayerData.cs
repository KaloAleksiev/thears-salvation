using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class PlayerData : ScriptableObject {
    [SerializeField] private double currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private double damage;
    [SerializeField] private int soulOrbs;
    [SerializeField] private double defenseMultiplier;

    public double CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public double Damage { get => damage; set => damage = value; }
    public int SoulOrbs { get => soulOrbs; set => soulOrbs = value; }
    public double DefenseMultiplier { get => defenseMultiplier; set => defenseMultiplier = value; }

    public void AddSoulOrbs(int amount) {
        this.SoulOrbs += amount;
    }
}
