using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenus : MonoBehaviour {
    public static bool GameIsPaused = false;

    public HUDManager hud;
    public GameObject pauseMenuUI;
    public GameObject upgradesMenuUI;

    private GameObject currentMenu;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                ResumeGame();
            } else {
                SetCurrentMenu(pauseMenuUI);
                PauseGame();
            }
        } else if (Input.GetKeyDown(KeyCode.E)) {
            if (GameIsPaused) {
                ResumeGame();
            } else {
                SetCurrentMenu(upgradesMenuUI);
                PauseGame();
            }
        }
    }

    public void ResumeGame() {
        currentMenu.SetActive(false);
        FreezeGame(false);
    }

    private void PauseGame() {
        currentMenu.SetActive(true);
        FreezeGame(true);
    }

    public void ExitGame() {
        SceneManager.LoadScene("MainMenu");
        FreezeGame(false);
    }

    private void FreezeGame(bool isGameFrozen) {
        if (isGameFrozen) {
            Time.timeScale = 0f;
            hud.player.GetComponent<Animator>().enabled = false;
            hud.player.playerMovement.enabled = false;
            hud.player.playerCombat.enabled = false;
        } else {
            Time.timeScale = 1f;
            hud.player.GetComponent<Animator>().enabled = true;
            hud.player.playerMovement.enabled = true;
            hud.player.playerCombat.enabled = true;
        }

        GameIsPaused = isGameFrozen;
    }

    public void SetCurrentMenu(GameObject menu) {
        currentMenu = menu;
    }
}
