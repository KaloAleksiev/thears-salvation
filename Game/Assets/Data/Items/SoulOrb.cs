using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SoulOrb", menuName = "SoulOrb")]
public class SoulOrb : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private Sprite image;
    [SerializeField] private int worth = 1;

    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private AudioClip ambientSound;

    public string Name { get => name; set => name = value; }
    public Sprite Image { get => image; set => image = value; }
    public int Worth { get => worth; set => worth = value; }
    public AudioClip PickupSound { get => pickupSound; set => pickupSound = value; }
    public AudioClip AmbientSound { get => ambientSound; set => ambientSound = value; }
}
