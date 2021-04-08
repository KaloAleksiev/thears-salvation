using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradesMenu : MonoBehaviour {
    public Player player;

    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI upgradesDoneText;

    public TextMeshProUGUI notEnoughSoulOrbsMessage;
    public TextMeshProUGUI maxUpgradesReachedMessage;

    private Upgrade selectedUpgrade;

    private void OnDisable() {
        // hide upgrades messages
        notEnoughSoulOrbsMessage.gameObject.SetActive(false);
        maxUpgradesReachedMessage.gameObject.SetActive(false);
    }

    public void SetUpgradeDetails(Upgrade upgrade) {
        selectedUpgrade = upgrade;

        iconImage.sprite = upgrade.icon;
        nameText.text = upgrade.name;
        descriptionText.text = upgrade.description;
        costText.text = upgrade.cost.ToString() + " " + Upgrade.CostText;
        SetUpgradesDoneText(upgrade);

        // hide upgrades messages
        notEnoughSoulOrbsMessage.gameObject.SetActive(false);
        maxUpgradesReachedMessage.gameObject.SetActive(false);
    }

    public void PurchaseUpgrade() {
        // check if max upgrades are reached
        if (selectedUpgrade.upgradesDone < selectedUpgrade.maxUpgrades) {
            // check if the player has enough soul orbs to purchase the upgrade
            if (PlayerManager.instance.playerData.soulOrbs < selectedUpgrade.cost) {
                // if the player does not have enough soul orbs, display appropriate message
                notEnoughSoulOrbsMessage.gameObject.SetActive(true);
            } else {
                // add negative amount of soul orbs from player (i.e. subtract soul orbs from player)
                player.addSoulOrbs.Invoke(-selectedUpgrade.cost);
                // increase the upgrades done for this upgrade
                selectedUpgrade.upgradesDone++;
                // update upgrade done UI text
                SetUpgradesDoneText(selectedUpgrade);
            }
        } else {
            // if max upgrades are already reached, display appropriate message
            maxUpgradesReachedMessage.gameObject.SetActive(true);
        }
    }

    private void SetUpgradesDoneText(Upgrade upgrade) {
        upgradesDoneText.text = upgrade.upgradesDone + "/" + upgrade.maxUpgrades + " " + Upgrade.UpgradesDoneText;
    }
}
