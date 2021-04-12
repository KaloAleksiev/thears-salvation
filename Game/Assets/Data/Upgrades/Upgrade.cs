using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class Upgrade : ScriptableObject {
    public static string CostText = "Soul Orbs per upgrade";
    public static string UpgradesDoneText = "upgrades done";

    [SerializeField] private Sprite icon;
    [SerializeField] private Color color;
    [SerializeField] private new string name;
    [SerializeField] private string description;
    [SerializeField] private int cost;
    [SerializeField] private int upgradesDone;
    [SerializeField] private int maxUpgrades;
    [SerializeField] private double modifier;

    public Sprite Icon { get => icon; set => icon = value; }
    public Color Color { get => color; set => color = value; }
    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public int Cost { get => cost; set => cost = value; }
    public int UpgradesDone { get => upgradesDone; set => upgradesDone = value; }
    public int MaxUpgrades { get => maxUpgrades; set => maxUpgrades = value; }
    public double Modifier { get => modifier; set => modifier = value; }
}
