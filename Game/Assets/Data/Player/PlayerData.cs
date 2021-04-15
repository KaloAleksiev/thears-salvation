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
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private AudioClip[] jumpSounds;
    [SerializeField] private AudioClip[] landSounds;
    [SerializeField] private AudioClip[] painSounds;
    [SerializeField] private AudioClip[] deathSounds;
    [SerializeField] private AudioClip[] spawnSounds;

    public double CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public double DamageMultiplier { get => damageMultiplier; set => damageMultiplier = value; }
    public int SoulOrbs { get => soulOrbs; set => soulOrbs = value; }
    public double DefenseMultiplier { get => defenseMultiplier; set => defenseMultiplier = value; }
    public Sword ActiveSword { get => activeSword; set => activeSword = value; }
    public int BurnTicks { get => burnTicks; set => burnTicks = value; }
    public float SlowModifier { get => slowModifier; set => slowModifier = value; }
    public AudioClip[] FootstepSounds { get => footstepSounds; set => footstepSounds = value; }
    public AudioClip[] JumpSounds { get => jumpSounds; set => jumpSounds = value; }
    public AudioClip[] LandSounds { get => landSounds; set => landSounds = value; }
    public AudioClip[] PainSounds { get => painSounds; set => painSounds = value; }
    public AudioClip[] DeathSounds { get => deathSounds; set => deathSounds = value; }
    public AudioClip[] SpawnSounds { get => spawnSounds; set => spawnSounds = value; }

    public void AddSoulOrbs(int amount) {
        this.SoulOrbs += amount;
    }
}
