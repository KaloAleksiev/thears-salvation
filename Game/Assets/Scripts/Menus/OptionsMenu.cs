using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;

    private Resolution[] resolutions;

    private void Start() {
        configureResolutionDropdown();
    }

    public void SetVolume(float volume) {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetFullscreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex) {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private void configureResolutionDropdown() {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++) {
            Resolution resolution = resolutions[i];

            string option = resolution.width + " x " + resolution.height + GetAspectRatio(resolution.width, resolution.height); //e.g. 1920 x 1080 [16:9]
            options.Add(option);

            // get index of current screen resolution
            if (resolution.width == Screen.currentResolution.width &&
                resolution.height == Screen.currentResolution.height) {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
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
