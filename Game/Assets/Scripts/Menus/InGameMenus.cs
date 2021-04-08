using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenus : MonoBehaviour {
    public static bool GameIsPaused = false;

    public Player player;
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
            player.GetComponent<Animator>().enabled = false;
            player.playerMovement.enabled = false;
            player.playerCombat.enabled = false;
        } else {
            Time.timeScale = 1f;
            player.GetComponent<Animator>().enabled = true;
            player.playerMovement.enabled = true;
            player.playerCombat.enabled = true;
        }

        GameIsPaused = isGameFrozen;
    }

    public void SetCurrentMenu(GameObject menu) {
        currentMenu = menu;
    }
}
