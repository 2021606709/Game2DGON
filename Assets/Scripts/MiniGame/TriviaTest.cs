using UnityEngine;

public class TriviaTest : MonoBehaviour
{
    [SerializeField] private TriviaQuestionFetcher fetcher;
    [SerializeField] private TriviaMiniGameUI triviaUI;

    public void OnClickTest()
    {
        fetcher.FetchQuestion((data) =>
        {
            triviaUI.ShowQuestion(data, (isCorrect) =>
            {
                if (isCorrect)
                    Debug.Log("Người chơi được thưởng!");
                else
                    Debug.Log("Người chơi bị trừ máu!");
            });
        });
    }
}
