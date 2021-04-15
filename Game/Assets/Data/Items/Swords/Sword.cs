using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sword", menuName = "Sword")]
public class Sword : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private Element element;
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeedModifier;
    [SerializeField] private Sprite image;
    [SerializeField] private AudioClip[] swingSounds;
    [SerializeField] private AudioClip[] hitSounds;
    [SerializeField] private AudioClip[] specialEffectSounds;
    [SerializeField] private AudioClip[] pickUpSounds;

    public string Name { get => name; set => name = value; }
    public Element Element { get => element; set => element = value; }
    public float Damage { get => damage; set => damage = value; }
    public float AttackSpeedModifier { get => attackSpeedModifier; set => attackSpeedModifier = value; }
    public Sprite Image { get => image; set => image = value; }
    public AudioClip[] SwingSounds { get => swingSounds; set => swingSounds = value; }
    public AudioClip[] HitSounds { get => hitSounds; set => hitSounds = value; }
    public AudioClip[] SpecialEffectSounds { get => specialEffectSounds; set => specialEffectSounds = value; }
    public AudioClip[] PickUpSounds { get => pickUpSounds; set => pickUpSounds = value; }
}
