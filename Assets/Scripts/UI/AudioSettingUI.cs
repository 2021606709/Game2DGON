using UnityEngine;
using UnityEngine.UI;

public class AudioSettingUI : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle sfxToggle;

    private void Start()
    {
        float musicVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 1f);
        bool musicOn = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        bool sfxOn = PlayerPrefs.GetInt("SFXEnabled", 1) == 1;

        musicVolumeSlider.SetValueWithoutNotify(musicVol);
        sfxVolumeSlider.SetValueWithoutNotify(sfxVol);
        musicToggle.SetIsOnWithoutNotify(musicOn);
        sfxToggle.SetIsOnWithoutNotify(sfxOn);
    }

    public void OnMusicVolumeChanged(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
    }
    public void OnSFXVolumeChanged(float value)
    {
        AudioManager.Instance.SetSFXVolume(value);
    }
    public void OnMusicToggleChanged(bool isOn)
    {
        AudioManager.Instance.ToggleMusic(isOn);
    }
    public void OnSFXToggleChanged(bool isOn)
    {
        AudioManager.Instance.ToggleSFX(isOn);
    }
    public void CloseSettings()
    {
        gameObject.SetActive(false);
    }
}
