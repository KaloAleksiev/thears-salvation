using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private double health;
    [SerializeField] private double damage;

    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private AudioClip[] painSounds;
    [SerializeField] private AudioClip[] deathSounds;
    [SerializeField] private AudioClip[] swingSounds;
    [SerializeField] private AudioClip[] hitSounds;

    public string Name { get => name; set => name = value; }
    public double Health { get => health; set => health = value; }
    public double Damage { get => damage; set => damage = value; }
    public AudioClip[] FootstepSounds { get => footstepSounds; set => footstepSounds = value; }
    public AudioClip[] PainSounds { get => painSounds; set => painSounds = value; }
    public AudioClip[] DeathSounds { get => deathSounds; set => deathSounds = value; }
    public AudioClip[] SwingSounds { get => swingSounds; set => swingSounds = value; }
    public AudioClip[] HitSounds { get => hitSounds; set => hitSounds = value; }
}
