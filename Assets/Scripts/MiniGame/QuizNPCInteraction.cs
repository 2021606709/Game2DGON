using UnityEngine;

public class QuizNPCInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject ePrompt;
    [SerializeField] private TriviaMiniGameUI triviaUI;
    [SerializeField] private TriviaQuestionFetcher fetcher;

    private bool playerInRange = false;
    private bool alreadAnswered = false;

    void Start()
    {
        if (ePrompt != null)
        {
            ePrompt.SetActive(false);
        }
    }

    void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.E) && !alreadAnswered)
        {
            fetcher.FetchQuestion((data) =>
            {
                triviaUI.ShowQuestion(data, (isCorrect) =>
                {
                    if (isCorrect)
                    {
                        Debug.Log("Trả lời đúng: +1 mạng!");
                        PlayerHealth.Instance.GainLife();
                        alreadAnswered = true;

                        Destroy(gameObject);
                    }
                    else
                    {
                        Debug.Log("Người chơi bị trừ máu!");
                        PlayerHealth.Instance.TakeDamage(5);
                        alreadAnswered = true;
                        ePrompt.SetActive(false);
                        this.enabled = false;
                    }
                });
            });
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !alreadAnswered)
        {
            ePrompt.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ePrompt.SetActive(false);
            playerInRange = false;
        }
    }
}
