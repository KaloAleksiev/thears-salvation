using UnityEngine;
using TMPro;

public class Narration : MonoBehaviour {
    public Player player;
    public TextMeshProUGUI[] textArray;

    public bool[] textArrayActive;

    private void Start() {
        textArrayActive = new bool[textArray.Length];

        for (int i = 0; i < textArrayActive.Length; i++) {
            textArrayActive[i] = false;
        }
    }

    private void Update() {
        if (Input.anyKeyDown) {
            if (!InGameMenus.GameIsPaused) {
                FreezeGame(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        NarrationElement narrationElement = collider.GetComponent<NarrationElement>();

        if (narrationElement) {
            if (!textArrayActive[narrationElement.number]) {
                FreezeGame(true);

                TextMeshProUGUI text = textArray[narrationElement.number];
                text.gameObject.SetActive(true);
                text.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 4, gameObject.transform.position.z);

                if (narrationElement.number > 0) textArray[narrationElement.number - 1].gameObject.SetActive(false);
                textArrayActive[narrationElement.number] = true;
            }
        }
    }

    private void FreezeGame(bool isGameFrozen) {
        if (isGameFrozen) {
            Time.timeScale = 0f;
            Cursor.visible = true;
            player.GetComponent<Animator>().enabled = false;
            player.playerMovement.enabled = false;
            player.playerCombat.enabled = false;
        } else {
            Time.timeScale = 1f;
            Cursor.visible = false;
            player.GetComponent<Animator>().enabled = true;
            player.playerMovement.enabled = true;
            player.playerCombat.enabled = true;
        }
    }
}
