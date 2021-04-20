using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Element", menuName = "Element")]
public class Element : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private Element weakerElement;
    [SerializeField] private Element strongerElement;
    [SerializeField] private double damageIncreaseModifier = 1.2f;
    [SerializeField] private double damageDecreaseModifier = 0.8f;

    public string Name { get => name; set => name = value; }
    public Element WeakerElement { get => weakerElement; set => weakerElement = value; }
    public Element StrongerElement { get => strongerElement; set => strongerElement = value; }
    public double DamageIncreaseModifier { get => damageIncreaseModifier; set => damageIncreaseModifier = value; }
    public double DamageDecreaseModifier { get => damageDecreaseModifier; set => damageDecreaseModifier = value; }
}
