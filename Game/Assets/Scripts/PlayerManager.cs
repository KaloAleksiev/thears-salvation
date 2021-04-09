using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public Player player;
    public Transform respawnPoint;
    public HUDManager hud;

    private void Start() {
        hud.updateSoulOrbsUI.Invoke();

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
        yield return new WaitForSeconds(2f);

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
        player.enablePlayerCollider.Invoke(isActivated);
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
        player.playerData.AddSoulOrbs(amount);
        hud.updateSoulOrbsUI.Invoke();
    }
}
