using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsUIManager : MonoBehaviour
{
    public Button backBtn;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider backGroundSlider;
    [SerializeField] Slider sfxSlider;

    private void Awake()
    {
        masterSlider.value = AudioManager.Instance.GetMasterVolume();
        backGroundSlider.value = AudioManager.Instance.GetBGMVolume();
        sfxSlider.value = AudioManager.Instance.GetSFXVolume();
        masterSlider.onValueChanged.AddListener(MasterVolumeChanged);
        backGroundSlider.onValueChanged.AddListener(BackGroundVolumeChanged);
        sfxSlider.onValueChanged.AddListener(sfxVolumeChanged);
    }

    void MasterVolumeChanged(float volume)
    {
        AudioManager.Instance.MasterVolume(volume);
    }

    void BackGroundVolumeChanged(float volume)
    {
        AudioManager.Instance.BGMVolume(volume);
    }

    void sfxVolumeChanged(float volume)
    {
        AudioManager.Instance.SFXVolume(volume);
    }
}
