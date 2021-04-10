using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class HUDManager : MonoBehaviour {
    public Player player;

    public TextMeshProUGUI soulOrbsUI;
    public TextMeshProUGUI soulOrbsUpgradesUI;

    public UnityEvent updateSoulOrbsUI;

    private void Start() {
        updateSoulOrbsUI.AddListener(UpdateSoulOrbsUI);
        UpdateSoulOrbsUI();
    }

    private void OnDestroy() {
        updateSoulOrbsUI.RemoveListener(UpdateSoulOrbsUI);
    }

    private void UpdateSoulOrbsUI() {
        soulOrbsUI.text = "Soul Orbs: " + player.playerData.SoulOrbs;
        soulOrbsUpgradesUI.text = player.playerData.SoulOrbs.ToString();
    }
}
