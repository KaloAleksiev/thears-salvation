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
        yield return new WaitForSeconds(1.1f);

        Activate(true);
    }

    private void Activate(bool isActivated) {
        //set body type of character rigidbody 2d
        if (isActivated) {
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        } else {
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        }

        //disable/enable character movement
        player.playerMovement.enabled = isActivated;
        player.playerCombat.enabled = isActivated;
        player.playerHealth.enabled = isActivated;
        player.health.enabled = isActivated;
    }

    private void Reset() {
        //reset character position
        player.transform.position = respawnPoint.position;
        player.playerMovement.RotateRight();

        player.ResetPlayerHealth();
        player.playRecoverAnimation.Invoke();
        player.setIntegerAnimator.Invoke("AnimState", 0);
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
