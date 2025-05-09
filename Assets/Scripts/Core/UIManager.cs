using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("HUD")]
    [SerializeField] private GameObject hud;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image healthFill;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenuUi;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverUi;

    [Header("Victory")]
    [SerializeField] private GameObject victoryUi;
    [SerializeField] private TextMeshProUGUI victoryScoreText;
    [SerializeField] private TextMeshProUGUI victoryTimeText;


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
        if (pauseMenuUi != null) pauseMenuUi.SetActive(false);
        if (gameOverUi != null) gameOverUi.SetActive(false);
        if (victoryUi != null) victoryUi.SetActive(false); 
        //StartCoroutine(DelayedAssign()); 
    }
    // private IEnumerator DelayedAssign()
    // {
    //     yield return new WaitForSeconds(0.05f); // hoặc yield return null nếu muốn chỉ delay 1 frame
    //     ReassignReferences();

    //     if (pauseMenuUi != null) pauseMenuUi.SetActive(false);
    //     if (gameOverUi != null) gameOverUi.SetActive(false);
    //     if (victoryUi != null) victoryUi.SetActive(false); 
    // }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.Instance.IsGameOver())
        {
            if (pauseMenuUi == null)
            {
                Debug.LogWarning("pauseMenuUi is null!");
                return;
            }

            if(pauseMenuUi.activeSelf)
            {
                HidePauseMenu();
            }
            else
            {
                ShowPauseMenu();    
            }
        }
    }
    // public void ReassignReferences()
    // {
    //     pauseMenuUi = GameObject.Find("PauseMenuUI");
    //     gameOverUi = GameObject.Find("GameOverUI");
    //     victoryUi = GameObject.Find("VictoryUI");
    //     hud = GameObject.Find("HUD");

    //     if (hud != null)
    //     {
    //         scoreText = GameObject.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();
    //         healthFill = GameObject.Find("Fill")?.GetComponent<Image>();
    //         healthText = GameObject.Find("HealthText")?.GetComponent<TextMeshProUGUI>();
    //     }

    //     victoryScoreText = GameObject.Find("VictoryScoreText")?.GetComponent<TextMeshProUGUI>();
    //     victoryTimeText = GameObject.Find("VictoryTimeText")?.GetComponent<TextMeshProUGUI>();
    //     Debug.Log("ReassignReferences() called");
    //     Debug.Log($"pauseMenuUi: {pauseMenuUi}");
    //     Debug.Log($"gameOverUi: {gameOverUi}");
    //     Debug.Log($"victoryUi: {victoryUi}");
    // }
    // private void OnEnable()
    // {
    //     SceneManager.sceneLoaded += OnSceneLoaded;
    // }

    // private void OnDisable()
    // {
    //     SceneManager.sceneLoaded -= OnSceneLoaded;
    // }

    // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     ReassignReferences();
    //     //  if (pauseMenuUi == null)
    //     // pauseMenuUi = GameObject.Find("PauseMenuUI");

    //     // if (gameOverUi == null)
    //     //     gameOverUi = GameObject.Find("GameOverUI");

    //     // if (victoryUi == null)
    //     //     victoryUi = GameObject.Find("VictoryUI");

    //     // if (hud == null)
    //     //     hud = GameObject.Find("HUD");

    //     // if (scoreText == null)
    //     //     scoreText = GameObject.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();

    //     // if (healthFill == null)
    //     //     healthFill = GameObject.Find("Fill")?.GetComponent<Image>();

    //     // if (healthText == null)
    //     //     healthText = GameObject.Find("HealthText")?.GetComponent<TextMeshProUGUI>();

    //     // if (victoryScoreText == null)
    //     //     victoryScoreText = GameObject.Find("VictoryScoreText")?.GetComponent<TextMeshProUGUI>();

    //     // if (victoryTimeText == null)
    //     //     victoryTimeText = GameObject.Find("VictoryTimeText")?.GetComponent<TextMeshProUGUI>();

    //     // Debug.Log("[UIManager] UI reassigned after scene load.");
    // }
    //HUD
    public void UpdateScore(int score)
    {
        if(scoreText != null)
        {
            scoreText.text = $"Score: {score:D3}";
        }
    }
    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        if(healthFill != null)
        {
            healthFill.fillAmount = Mathf.Clamp01((float)currentHealth / maxHealth);
        }
        if(healthText != null)
        {
            healthText.text = $"{currentHealth}/{maxHealth}";
        }
    }
    //Pause Menu
    public void ShowPauseMenu()
    {
        if (pauseMenuUi != null)
        {
            pauseMenuUi.SetActive(true);
            if (hud != null) hud.SetActive(false);
            Time.timeScale = 0;
        }
        else
        {
            Debug.LogWarning("PauseMenuUI is null!");
        }
        Debug.Log($"[UIManager] ShowPauseMenu() called - pauseMenuUi: {(pauseMenuUi == null ? "null" : "ok")}");
    }
    public void HidePauseMenu()
    {
        if (pauseMenuUi != null)
        {
            pauseMenuUi.SetActive(false);
            if (hud != null) hud.SetActive(true);
            Time.timeScale = 1;
        }
    }
    //Game Over
    public void ShowGameOverUi()
    {
        if (gameOverUi != null)
        {
            gameOverUi.SetActive(true);
            if (hud != null) hud.SetActive(false);
        }
        else
        {
            Debug.LogWarning("GameOverUI is null!");
        }
        Debug.Log($"[UIManager] ShowGameOverUi() called - gameOverUi: {(gameOverUi == null ? "null" : "ok")}");
    }
    //Victory 
    public void ShowVictoryUi(float time, int score)
    {
        if(hud != null)
        {
            hud.SetActive(false);
        }

        if(victoryUi != null)
        {
            victoryUi.SetActive(true);
            if(victoryScoreText != null)
            {
                victoryScoreText.text = $"Score: {score:D3}";
            }
            if(victoryTimeText != null)
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds(time);
                victoryTimeText.text = $"Time: {timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
            }
            Time.timeScale = 0;
        }
    }
    public void HideAllUI()
    {
        if(hud != null)
        {
            hud.SetActive(false);
        }
        if(pauseMenuUi != null)
        {
            pauseMenuUi.SetActive(false);
        }
        if(gameOverUi != null)
        {
            gameOverUi.SetActive(false);
        }
        if(victoryUi != null)
        {
            victoryUi.SetActive(false);
        }
    }
    public void ShowHud()
    {
        if(hud != null)
        {
            hud.SetActive(true);
        }
    }
}
