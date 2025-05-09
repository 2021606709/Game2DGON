using System.Collections;
using UnityEngine;

public class SteamVent : MonoBehaviour
{
    [SerializeField] private Animator ventAnimator;
    [SerializeField] private GameObject hitBox;
    [SerializeField] private float delayBeforeStart = 0f;
    [SerializeField] private float delayBetweenShots = 1.5f;

    void Start()
    {
        StartCoroutine(SteamLoop());   
    }

    private IEnumerator SteamLoop()
    {
        yield return new WaitForSeconds(delayBeforeStart);
        while (true)
        {
            ventAnimator.Play("SteamVent", 0, 0f);
                yield return new WaitForSeconds(ventAnimator.GetCurrentAnimatorStateInfo(0).length);
                yield return new WaitForSeconds(delayBetweenShots);
        }
    }

    public void EnableHitBox()
    {
        if(hitBox != null)
        {
            hitBox.SetActive(true);
        }
    }

    public void DisableHitBox()
    {
        if(hitBox != null)
        {
            hitBox.SetActive(false);
        }
    }
}
