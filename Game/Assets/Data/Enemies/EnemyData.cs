using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private double health;
    [SerializeField] private double damage;

    public string Name { get => name; set => name = value; }
    public double Health { get => health; set => health = value; }
    public double Damage { get => damage; set => damage = value; }
}
