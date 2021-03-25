using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class LevelManager : MonoBehaviour {
    public static LevelManager instance;
    
    public Transform respawnPoint;

    [Header("Soul Orbs")]
    public int soulOrbs = 0;
    public Text soulOrbsUI;

    private void Awake() {
        instance = this;
    }

    public void Respawn(GameObject player) {
        player.transform.position = respawnPoint.position;
        player.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void IncreaseSoulOrbs(int amount) {
        soulOrbs += amount;
        soulOrbsUI.text = "Soul Orbs: " + amount;
    }
}
