using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonSFX : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(PlayClickSound);
    }
    void PlayClickSound()
    {
        if (clickSound != null)
        {
            AudioManager.Instance.PlaySFX(clickSound);
        }
        else
        {
            AudioManager.Instance.PlayDefaultClickSound();
        }
    }
}
