using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour {
    public static PlayerManager instance;

    public Player player;
    public Transform respawnPoint;

    [Header("Soul Orbs")]
    public PlayerData playerData;
    public TextMeshProUGUI soulOrbsUI;
    public TextMeshProUGUI soulOrbsUpgradesUI;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        UpdateSoulOrbsUI();

        player.respawn.AddListener(Respawn);
        player.addSoulOrbs.AddListener(AddSoulOrbs);
    }

    private void OnDestroy() {
        player.respawn.RemoveListener(Respawn);
        player.addSoulOrbs.RemoveListener(AddSoulOrbs);
    }

    public void Respawn() {
        StartCoroutine(RespawnRoutine());
    }

    public IEnumerator RespawnRoutine() {
        Activate(false);
        yield return new WaitForSeconds(2);

        Reset();
        Activate(true);
    }

    private void Activate(bool isActivated) {
        //set body type of character rigidbody 2d
        if (isActivated) {
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        } else {
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }

        //disable/enable character box collider 2d
        player.GetComponent<BoxCollider2D>().enabled = isActivated;

        //disable/enable character movement
        player.playerMovement.enabled = isActivated;

        //disable/enable spriterenderer; TODO show corpse sprite while character is deactivated
        foreach (SpriteRenderer sr in player.GetComponentsInChildren<SpriteRenderer>()) sr.enabled = isActivated;
    }

    private void Reset() {
        //reset character position
        player.transform.position = respawnPoint.position;
        player.playerMovement.RotateRight();

        player.ResetPlayerHealth();
    }

    public void AddSoulOrbs(int amount) {
        playerData.AddSoulOrbs(amount);
        UpdateSoulOrbsUI();
    }

    private void UpdateSoulOrbsUI() {
        soulOrbsUI.text = "Soul Orbs: " + playerData.soulOrbs;
        soulOrbsUpgradesUI.text = playerData.soulOrbs.ToString();
    }
}
