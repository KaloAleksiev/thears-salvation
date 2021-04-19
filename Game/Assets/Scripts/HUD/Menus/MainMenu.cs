using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour {
    public Settings settings;

    [SerializeField] private AudioMixer masterVolume;

    private void Start() {
        if (settings.ResolutionIndex == -1) {
            SetCurrentResolutionIndex();
        }

        Resolution resolution = Screen.resolutions[settings.ResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Screen.fullScreen = settings.IsFullScreen;
        masterVolume.SetFloat("MasterVolume", settings.MasterVolumeValue);
        masterVolume.SetFloat("MusicVolume", settings.MusicVolumeValue);
        masterVolume.SetFloat("SoundsVolume", settings.SoundsVolumeValue);
    }

    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame() {
        Application.Quit();
    }

    private void SetCurrentResolutionIndex() {
        int currentResolutionIndex = 0;

        for (int i = 0; i < Screen.resolutions.Length; i++) {
            Resolution resolution = Screen.resolutions[i];

            // get index of current screen resolution
            if (resolution.width == Screen.currentResolution.width &&
                resolution.height == Screen.currentResolution.height) {
                currentResolutionIndex = i;
            }
        }

        settings.ResolutionIndex = currentResolutionIndex;
    }
}
