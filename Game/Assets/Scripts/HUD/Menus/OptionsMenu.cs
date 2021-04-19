using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {
    public Settings settings;
    public AudioMixer masterVolume;
    public Dropdown resolutionDropdown;

    public Toggle fullScreenToggle;
    public GameObject fullScreenOnButton;
    public GameObject fullScreenOffButton;
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider soundsVolumeSlider;

    private Resolution[] resolutions;

    private void Start() {
        RefreshOptionsUI();
    }

    private void RefreshOptionsUI() {
        // resolution
        ConfigureResolutionsDropdown();
        resolutionDropdown.value = settings.ResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // full screen
        fullScreenToggle.isOn = settings.IsFullScreen;
        fullScreenOnButton.SetActive(settings.IsFullScreen);
        fullScreenOffButton.SetActive(!settings.IsFullScreen);

        // volume
        masterVolumeSlider.value = settings.MasterVolumeValue;
        musicVolumeSlider.value = settings.MusicVolumeValue;
        soundsVolumeSlider.value = settings.SoundsVolumeValue;
    }

    public void SetMasterVolume(float volume) {
        masterVolume.SetFloat("MasterVolume", volume);
        settings.MasterVolumeValue = volume;
    }

    public void SetMusicVolume(float volume) {
        masterVolume.SetFloat("MusicVolume", volume);
        settings.MusicVolumeValue = volume;
    }

    public void SetSoundsVolume(float volume) {
        masterVolume.SetFloat("SoundsVolume", volume);
        settings.SoundsVolumeValue = volume;
    }

    public void SetFullscreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
        settings.IsFullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex) {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        settings.ResolutionIndex = resolutionIndex;
    }

    private void ConfigureResolutionsDropdown() {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++) {
            Resolution resolution = resolutions[i];

            string option = resolution.width + " x " + resolution.height + GetAspectRatio(resolution.width, resolution.height); //e.g. 1920 x 1080 [16:9]
            options.Add(option);
        }

        resolutionDropdown.AddOptions(options);
    }

    // Function to reduce a fraction to its lowest form
    // Source: https://www.geeksforgeeks.org/reduce-the-fraction-to-its-lowest-form/
    private string GetAspectRatio(int x, int y) {
        int d = Gcd(x, y);

        x /= d;
        y /= d;

        return " [" + x + ":" + y + "]";
    }

    private int Gcd(int a, int b) {
        if (b == 0)
            return a;
        return Gcd(b, a % b);
    }
}
