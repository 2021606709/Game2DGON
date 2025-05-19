using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Transform player;
    [SerializeField] private PlayerStatsSO playerStatsSO;

    private bool isGameOver = false;
    private int score = 0;
    private float levelStartTime;

    private Vector3 lastCheckPointPosition;
    private int checkPointHealth;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        //Save the current level index to PlayerPrefs when the game starts
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("LastPlayedLevel", currentLevel);
        PlayerPrefs.Save();

        isGameOver = false;
        score = 0;
        UIManager.Instance.UpdateScore(score);
        levelStartTime = Time.time;

        lastCheckPointPosition = player.position;
        checkPointHealth = playerStatsSO.maxHealth;
    }

    // private void Onable()
    // {
    //     SceneManager.sceneLoaded += OnSceneLoaded;
    // }

    // private void Osable()
    // {
    //     SceneManager.sceneLoaded -= OnSceneLoaded;         
    // }

    // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     UIManager.Instance?.ReassignReferences();
    // }

    public bool IsGameOver()
    {
        return isGameOver;
    }
    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
        UIManager.Instance.ShowGameOverUi();
    }
    public void RestartGame()
    {
        isGameOver = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadNextLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
        SceneManager.LoadScene("Level" + (currentLevel + 1));
    }
    public void AddScore(int points)
    {
        if (!isGameOver)
        {
            score += points;
            UIManager.Instance.UpdateScore(score);
        }
    }
    public void Victory()
    {
        Time.timeScale = 0;
        float timeTaken = Time.time - levelStartTime;
        UIManager.Instance.ShowVictoryUi(timeTaken, score);

        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        if(currentLevel >= unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", currentLevel + 1);
            PlayerPrefs.Save();
        }
    }

    // public void LoadNextLevel()
    // {
    //     Time.timeScale = 1;
    //     int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    //     int nextSceneIndex = currentSceneIndex + 1;
    //     if(nextSceneIndex < SceneManager.sceneCountInBuildSettings)
    //     {
    //         SceneManager.LoadScene(nextSceneIndex);
    //     }
    //     else
    //     {
    //         LoadMainMenu();
    //     }
    // }
    public void SetCheckPoint(Vector3 position, int currentHealth)
    {
        lastCheckPointPosition = position + new Vector3(0, 0.5f, 0);
        checkPointHealth = currentHealth;
    }

    public Vector3 GetLastCheckPointPosition()
    {
        return lastCheckPointPosition;
    }

    public int GetCheckPointHealth()
    {
        return checkPointHealth;
    }
}
