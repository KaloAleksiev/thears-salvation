using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Settings", menuName = "Settings")]
public class Settings : ScriptableObject {
    [SerializeField] private int resolutionIndex;
    [SerializeField] private bool isFullScreen;
    [SerializeField] private float masterVolumeValue;
    [SerializeField] private float musicVolumeValue;
    [SerializeField] private float soundsVolumeValue;

    public int ResolutionIndex { get => resolutionIndex; set => resolutionIndex = value; }
    public bool IsFullScreen { get => isFullScreen; set => isFullScreen = value; }
    public float MasterVolumeValue { get => masterVolumeValue; set => masterVolumeValue = value; }
    public float MusicVolumeValue { get => musicVolumeValue; set => musicVolumeValue = value; }
    public float SoundsVolumeValue { get => soundsVolumeValue; set => soundsVolumeValue = value; }
}
