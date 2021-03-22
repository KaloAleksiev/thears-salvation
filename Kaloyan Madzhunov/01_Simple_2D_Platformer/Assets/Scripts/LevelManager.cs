﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class LevelManager : MonoBehaviour {
    public static LevelManager instance;
    
    public Transform respawnPoint;
    public GameObject playerPrefab;

    public CinemachineVirtualCameraBase cameraBase;

    [Header("Soul Orbs")]
    public int soulOrbs = 0;
    public Text soulOrbsUI;

    private void Awake() {
        instance = this;
    }

    public void Respawn() {
        GameObject player = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
        cameraBase.Follow = player.transform;
    }

    public void IncreaseSoulOrbs(int amount) {
        soulOrbs += amount;
        soulOrbsUI.text = "Soul Orbs: " + amount;
    }
}
