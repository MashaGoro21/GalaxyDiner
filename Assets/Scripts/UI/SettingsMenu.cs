using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider musicVolumeSlider;

    private void Start()
    {
        musicVolumeSlider.value = MusicPlayer.Instance.GetVolume();
        musicVolumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    public void OnVolumeChanged(float value)
    {
        MusicPlayer.Instance.SetVolume(value);
    }
}
