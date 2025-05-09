using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelSelectManager : MonoBehaviour
{
    public Button[] levelButtons;
    public GameObject[] lockPanels;

    void Start()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i < unlockedLevel)
            {
                levelButtons[i].interactable = true;
                lockPanels[i].SetActive(false);
            }
            else
            {
                levelButtons[i].interactable = false;
                lockPanels[i].SetActive(true);
            }
        }    
    }
    public void LoadLevel(int levelIndex)
    {
        // Load the specified level
        SceneManager.LoadScene("Level"+levelIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
