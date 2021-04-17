using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ButtonAudio : MonoBehaviour {
    [SerializeField] private AudioClip selectSound;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip upgradeSound;

    private AudioSource audioSource;

    void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlaySelectSound() {
        PlaySound(selectSound);
    }

    public void PlayClickSound() {
        PlaySound(clickSound);
    }

    public void PlayUpgradeSound() {
        PlaySound(upgradeSound);
    }

    private void PlaySound(AudioClip sound) {
        audioSource.PlayOneShot(sound);
    }
}
