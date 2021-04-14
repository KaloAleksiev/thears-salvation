using UnityEngine;

public class PlayerUpgrades : MonoBehaviour {
    public Player player;

    private void Start() {
        player.healthBoost.AddListener(BoostHealth);
        player.ultraDefense.AddListener(AddUltraDefense);
        player.damageEnhance.AddListener(EnhanceDamage);
    }

    private void OnDestroy() {
        player.healthBoost.RemoveListener(BoostHealth);
        player.ultraDefense.RemoveListener(AddUltraDefense);
        player.damageEnhance.RemoveListener(EnhanceDamage);
    }

    private void BoostHealth(double modifier) {
        player.playerData.MaxHealth += (int)modifier;
        player.playerData.CurrentHealth += modifier;
        player.setPlayerHealth.Invoke();
    }

    private void AddUltraDefense(double modifier) {
        player.playerData.DefenseMultiplier -= modifier;
    }

    private void EnhanceDamage(double modifier) {
        player.playerData.DamageMultiplier += modifier;
    }
}
