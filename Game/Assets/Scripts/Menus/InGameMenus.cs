using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenus : MonoBehaviour {
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject upgradesMenuUI;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        } else if (Input.GetKeyDown(KeyCode.E)) {
            if (GameIsPaused) {
                upgradesMenuUI.SetActive(false);
                FreezeGame(false);
            } else {
                upgradesMenuUI.SetActive(true);
                FreezeGame(true);
            }
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        FreezeGame(false);
    }

    private void Pause() {
        pauseMenuUI.SetActive(true);
        FreezeGame(true);
    }

    public void LoadOptions() {
        pauseMenuUI.SetActive(false);
        FreezeGame(false);
    }

    public void Exit() {
        FreezeGame(false);
        SceneManager.LoadScene("MainMenu");
    }

    private void FreezeGame(bool isGameFrozen) {
        if (isGameFrozen) {
            Time.timeScale = 0f;
        } else {
            Time.timeScale = 1f;
        }

        GameIsPaused = isGameFrozen;
    }
}
