using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class InGameMenus : MonoBehaviour {
    public static bool GameIsPaused = false;

    public HUDManager hud;
    public GameObject pauseMenuUI;
    public GameObject upgradesMenuUI;
    public AudioSource music;

    private GameObject currentMenu;
    private AudioSource audioSource;

    void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            CheckGamePausedAndSetMenu(pauseMenuUI);
        } else if (Input.GetKeyDown(KeyCode.E)) {
            CheckGamePausedAndSetMenu(upgradesMenuUI);
        }
    }

    private void CheckGamePausedAndSetMenu(GameObject menu) {
        if (GameIsPaused) {
            ResumeGame();
            music.UnPause();
        } else {
            audioSource.Play();
            music.Pause();
            SetCurrentMenu(menu);
            PauseGame();
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
            Cursor.visible = true;
            Time.timeScale = 0f;
            hud.player.GetComponent<Animator>().enabled = false;
            hud.player.playerMovement.enabled = false;
            hud.player.playerCombat.enabled = false;
        } else {
            Cursor.visible = false;
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
