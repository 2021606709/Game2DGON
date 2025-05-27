using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using SimpleJSON;
using Unity.VisualScripting;

public class TriviaQuestionFetcher : MonoBehaviour
{
    // void Start()
    // {
    //     FetchQuestion((data) =>
    //     {
    //         Debug.Log("Question: " + data.question);
    //         foreach (var option in data.options)
    //         {
    //             Debug.Log("Option: " + option);
    //         }
    //     });
    // }
    public void FetchQuestion(System.Action<TriviaData> callBack)
    {
        StartCoroutine(GetQuestionCoroutine(callBack));
    }

    IEnumerator GetQuestionCoroutine(System.Action<TriviaData> callBack)
    {
        string url = "https://opentdb.com/api.php?amount=1&category=17&type=multiple";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            var json = JSON.Parse(request.downloadHandler.text);
            var result = json["results"][0];

            string question = System.Net.WebUtility.HtmlDecode(result["question"]);
            string correct = System.Net.WebUtility.HtmlDecode(result["correct_answer"]);

            List<string> options = new List<string>();
            options.Add(correct);

            foreach (var wrong in result["incorrect_answers"].AsArray)
            {
                options.Add(System.Net.WebUtility.HtmlDecode(wrong.Value));
            }

            for (int i = 0; i < options.Count; i++)
            {
                // Shuffle the options
                int rand = Random.Range(i, options.Count);
                (options[i], options[rand]) = (options[rand], options[i]);
            }

            callBack?.Invoke(new TriviaData
            {
                question = question,
                correctAnswer = correct,
                options = options
            });
        }else
        {
            Debug.LogError("Failed to fetch trivia question: " + request.error);
            callBack?.Invoke(null);
        }
    }
}

public class TriviaData
{
    public string question;
    public string correctAnswer;
    public List<string> options;
}
