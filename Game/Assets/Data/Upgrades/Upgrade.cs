using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class Upgrade : ScriptableObject {
    public static string CostText = "Soul Orbs per upgrade";
    public static string UpgradesDoneText = "upgrades done";

    public Sprite icon;
    public new string name;
    public string description;
    public int cost;
    public int upgradesDone;
    public int maxUpgrades;
}
