using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Elemental", menuName = "Elemental")]
public class Elemental : EnemyData
{
    [SerializeField] private Element element;

    public Element Element { get => element; set => element = value; }
}
