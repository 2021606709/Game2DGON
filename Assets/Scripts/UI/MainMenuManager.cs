using UnityEngine;
public class MainMenuManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private GameObject guideUI;

    [SerializeField] private AudioClip menuMusic;

    private void Start()
    {
        AudioManager.Instance.SetCurrentMusicClip(menuMusic);
        AudioManager.Instance.LoadAudioSettings();
        //PlayerPrefs.SetInt("UnlockedLevel", 1); // Reset unlocked level for testing  
    }
    // private void Awake()
    // {
    //     ResetAudioSettings();
    // }
    // public void ResetAudioSettings()
    // {
    //     PlayerPrefs.DeleteKey("MusicEnabled");
    //     PlayerPrefs.DeleteKey("SFXEnabled");
    //     PlayerPrefs.DeleteKey("MusicVolume");
    //     PlayerPrefs.DeleteKey("SFXVolume");
    //     PlayerPrefs.SetInt("HasResetAudio", 1);
    //     PlayerPrefs.Save();
    //     Debug.Log("[AudioManager] PlayerPrefs reset complete.");
    // }
    public void OnClickStartGame()
    {
        //Load the last game scence 
        int unlockedLevel = PlayerPrefs.GetInt("LastPlayedLevel", 1);
        SceneLoader.LoadScene("Level" + unlockedLevel);
    }

    public void OnClickSelectLevel()
    {
        //Load the level selection scene
        SceneLoader.LoadScene("LevelSelect");
    }

    public void OnClickSettings()
    {
        //Load the settings scene
        mainMenuUI.SetActive(false);
        settingsUI.SetActive(true);
    }
    public void OnClickBackToMainMenu()
    {
        //Load the main menu scene
        settingsUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
    public void OnClickGuide()
    {
        //Load the guide scene
        mainMenuUI.SetActive(false);
        guideUI.SetActive(true);
    }
    public void OnClickBackToMainMenuFromGuide()
    {
        //Load the main menu scene
        guideUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
    public void OnClickQuitGame()
    {
        //Quit the game
        SceneLoader.QuitGame();
    }
}
