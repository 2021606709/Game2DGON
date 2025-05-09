using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips Default")]
    [SerializeField] private AudioClip defaultClickSound;

    private AudioClip currentMusicClip; 
    private bool musicEnabled = true;
    private bool sfxEnabled = true;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadAudioSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        Debug.Log($"[AudioManager] PlayMusic called - Enabled: {musicEnabled} | Clip: {(clip ? clip.name : "null")}");

        if(!musicEnabled || clip == null || musicSource == null)
        {
            Debug.LogWarning("[AudioManager] PlayMusic aborted (invalid state)");
            return;
        }

        currentMusicClip = clip;

        if(musicSource.clip != clip)
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
            Debug.Log("[AudioManager] Music started.");
        }
    }
    public void PlaySFX(AudioClip clip)
    {
        if (sfxEnabled && clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
             Debug.Log($"[AudioManager] PlaySFX: {clip.name}");
        }
        else
        {
            Debug.LogWarning("[AudioManager] PlaySFX aborted (invalid state)");
        }
    }

    public void PlayDefaultClickSound()
    {
        PlaySFX(defaultClickSound);
    }

    public void SetMusicVolume(float volume)
    {
        if(musicSource != null)
        {
            musicSource.volume = volume;
        }
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        if(sfxSource != null)
        {
            sfxSource.volume = volume;
        }
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
    public void ToggleMusic(bool isOn)
    {
        musicEnabled = isOn;

        if (musicSource != null)
        {
            musicSource.mute =  !isOn;

            if(isOn)
            {
                if (!musicSource.isPlaying && currentMusicClip != null)
                {
                    musicSource.clip = currentMusicClip;
                    musicSource.loop = true;
                    musicSource.Play();
                    Debug.Log("[AudioManager] Music resumed.");
                }
            }
            else
            {
                musicSource.Stop();
                Debug.Log("[AudioManager] Music stopped.");
            }
        }
        PlayerPrefs.SetInt("MusicEnabled", isOn ? 1 : 0);
    }
    public void ToggleSFX(bool isOn)
    {
        sfxEnabled = isOn;
        PlayerPrefs.SetInt("SFXEnabled", isOn ? 1 : 0);
    }
    public void LoadAudioSettings()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        bool musicOn = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        bool sfxOn = PlayerPrefs.GetInt("SFXEnabled", 1) == 1;

        Debug.Log($"[AudioManager] LoadAudioSettings | Music: {musicOn} ({musicVolume}) | SFX: {sfxOn} ({sfxVolume})");

        SetMusicVolume(musicVolume);
        SetSFXVolume(sfxVolume);
        
        ToggleMusic(musicOn); 
        ToggleSFX(sfxOn);

        if (musicOn && currentMusicClip != null && !musicSource.isPlaying)
        {
            PlayMusic(currentMusicClip);
        }
    }
    public void SetCurrentMusicClip(AudioClip clip)
    {
        currentMusicClip = clip;
    }
}
