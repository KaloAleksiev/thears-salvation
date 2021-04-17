using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class UpgradesMenu : MonoBehaviour {
    public HUDManager hud;

    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI upgradesDoneText;

    public TextMeshProUGUI requirementMessage;
    public TextMeshProUGUI maxUpgradesReachedMessage;
    public GameObject upgradeButton;
    public GameObject upgradeConfirmation;

    private Dictionary<string, UpgradeUnityEvent> upgradeEvents;
    private Upgrade selectedUpgrade;

    private void Start() {
        upgradeEvents = new Dictionary<string, UpgradeUnityEvent>();

        upgradeEvents.Add("Health Boost", hud.player.healthBoost);
        upgradeEvents.Add("Ultra Defense", hud.player.ultraDefense);
        upgradeEvents.Add("Damage Enhance", hud.player.damageEnhance);
    }

    private void OnDisable() {
        // hide upgrades messages
        requirementMessage.gameObject.SetActive(false);
        maxUpgradesReachedMessage.gameObject.SetActive(false);
    }

    public void SetUpgradeDetails(Upgrade upgrade) {
        selectedUpgrade = upgrade;

        iconImage.sprite = upgrade.Icon;
        nameText.text = upgrade.Name;
        nameText.color = upgrade.Color;
        descriptionText.text = upgrade.Description;
        costText.text = upgrade.Cost.ToString() + " " + Upgrade.CostText;
        requirementMessage.text = "Requires " + upgrade.Cost.ToString() + " Soul Orbs";
        SetUpgradesDoneText(upgrade);

        // hide upgrades messages
        requirementMessage.gameObject.SetActive(false);
        maxUpgradesReachedMessage.gameObject.SetActive(false);
    }

    public void ValidateUpgradePurchase() {
        // check if max upgrades are reached
        if (selectedUpgrade.UpgradesDone < selectedUpgrade.MaxUpgrades) {
            // check if the player has enough soul orbs to purchase the upgrade
            if (hud.player.playerData.SoulOrbs < selectedUpgrade.Cost) {
                // if the player does not have enough soul orbs, display appropriate message
                requirementMessage.gameObject.SetActive(true);
            } else {
                upgradeConfirmation.SetActive(true);
                upgradeButton.SetActive(false);
            }
        } else {
            // if max upgrades are already reached, display appropriate message
            maxUpgradesReachedMessage.gameObject.SetActive(true);
        }
    }

    public void PurchaseUpgrade() {
        // add negative amount of soul orbs from player (i.e. subtract soul orbs from player)
        hud.player.addSoulOrbs.Invoke(-selectedUpgrade.Cost);

        // apply upgrade to player
        UpgradeUnityEvent upgradeEvent = upgradeEvents[selectedUpgrade.Name];

        if (upgradeEvent != null) {
            upgradeEvent.Invoke(selectedUpgrade.Modifier);
        }

        // increase the upgrades done for this upgrade
        selectedUpgrade.UpgradesDone++;
        
        // update upgrade done UI text
        SetUpgradesDoneText(selectedUpgrade);

        upgradeConfirmation.SetActive(false);
        upgradeButton.SetActive(true);
    }

    private void SetUpgradesDoneText(Upgrade upgrade) {
        upgradesDoneText.text = upgrade.UpgradesDone + "/" + upgrade.MaxUpgrades + " " + Upgrade.UpgradesDoneText;
    }
}