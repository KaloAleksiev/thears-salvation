using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {
    public static PlayerManager instance;

    public Player player;
    public Transform respawnPoint;

    [Header("Soul Orbs")]
    public int soulOrbs = 0;
    public Text soulOrbsUI;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        player.respawn.AddListener(Respawn);
    }

    private void OnDestroy() {
        player.respawn.RemoveListener(Respawn);
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

    public void IncreaseSoulOrbs(int amount) {
        soulOrbs += amount;
        soulOrbsUI.text = "Soul Orbs: " + amount;
    }
}
