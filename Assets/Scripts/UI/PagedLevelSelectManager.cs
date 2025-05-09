using UnityEngine;
using UnityEngine.UI;

public class PagedLevelSelectManager : MonoBehaviour
{
    [Header("Level Pages")]
    [SerializeField] private GameObject[] pages; 

    [Header("Buttons")]
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;

    private int currentPage = 0; 

    void Start()
    {
        ShowPage(currentPage); 
    }

    public void ShowNextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            ShowPage(currentPage);
        }
    }

    public void ShowPreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            ShowPage(currentPage);
        }
    }

    private void ShowPage(int pageIndex)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == pageIndex);
        }

        previousButton.interactable = pageIndex > 0; 
        nextButton.interactable = pageIndex < pages.Length - 1; 
        // previousButton.gameObject.SetActive(pageIndex > 0);
        // nextButton.gameObject.SetActive(pageIndex < pages.Length - 1);
    }
}
