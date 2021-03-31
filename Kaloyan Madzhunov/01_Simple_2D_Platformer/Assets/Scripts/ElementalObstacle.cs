using System.Collections;
using UnityEngine;

public class ElementalObstacle : MonoBehaviour {
    public Player player;

    public float horizontalKnock;
    public float verticalKnock;

    private bool isLeft;

    private void OnCollisionEnter2D(Collision2D collision) {
        player.playerHealth.Damage(Constants.BASIC_SWORD_DMG);

        isLeft = player.transform.position.x < transform.position.x;
        player.playerMovement.Knockback(horizontalKnock * (isLeft ? -1 : 1), verticalKnock);
    }
}
