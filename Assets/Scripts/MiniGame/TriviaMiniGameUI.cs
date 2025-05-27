using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class TriviaMiniGameUI : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button[] optionButtons;

    private string correctAnswer;
    private System.Action<bool> onAnswerCallback;

    public void ShowQuestion(TriviaData data, System.Action<bool> callback)
    {
        gameObject.SetActive(true);
        questionText.text = data.question;
        correctAnswer = data.correctAnswer;
        onAnswerCallback = callback;

        Time.timeScale = 0f;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            string answer = data.options[i];
            optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answer;

            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => OnAnswerSelected(answer));
        }
    }

    void OnAnswerSelected(string selectedAnswer)
    {
        bool isCorrect = selectedAnswer == correctAnswer;
        Debug.Log(isCorrect ? "Trả lời đúng!" : "Trả lời sai!");
        Time.timeScale = 1f;
        onAnswerCallback?.Invoke(isCorrect);
        gameObject.SetActive(false);
    }
}
