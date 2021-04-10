﻿using UnityEngine;

public class PlayerCollectibles : MonoBehaviour {
    public Player player;

    private void OnTriggerEnter2D(Collider2D collider) {
        Collectible collectible = collider.GetComponent<Collectible>();

        if (collectible) {
            int collectibleWorth = collectible.worth;
            player.addSoulOrbs.Invoke(collectibleWorth);

            Destroy(collider.gameObject);
        }
    }
}