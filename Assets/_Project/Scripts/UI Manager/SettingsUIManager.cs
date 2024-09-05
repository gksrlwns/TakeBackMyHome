using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUIManager : MonoBehaviour
{
    public Button backBtn;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider backGroundSlider;
    [SerializeField] Slider sfxSlider;

    private void Awake()
    {
        masterSlider.onValueChanged.AddListener(MasterVolumeChanged);
        backGroundSlider.onValueChanged.AddListener(BackGroundVolumeChanged);
        sfxSlider.onValueChanged.AddListener(sfxVolumeChanged);
    }

    void MasterVolumeChanged(float volume)
    {
        BackGroundVolumeChanged(volume);
        sfxVolumeChanged(volume);
    }

    void BackGroundVolumeChanged(float volume)
    {
        AudioManager.Instance.bgmPlayer.volume = volume;
    }

    void sfxVolumeChanged(float volume)
    {
        for (int i = 0; i < AudioManager.Instance.sfxPlayers.Length; i++)
        {
            AudioManager.Instance.sfxPlayers[i].volume = volume;
        }
    }
}
