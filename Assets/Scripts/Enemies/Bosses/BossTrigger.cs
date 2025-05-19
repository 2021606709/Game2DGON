using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] private GameObject bossInstanceInScene;
    [SerializeField] private AudioClip bossMusic;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(triggered || !collision.CompareTag("Player"))
        {
            return;
        }
        
        triggered = true;
        GetComponent<Collider2D>().enabled = false;

        if(bossInstanceInScene != null)
        {
            bossInstanceInScene.SetActive(true);
            Debug.Log("[BossTrigger] Boss spawned.");
        }
        else
        {
            Debug.LogWarning("[BossTrigger] Boss prefab or spawn point not set.");
        }

        AudioManager.Instance.SetCurrentMusicClip(bossMusic);
        AudioManager.Instance.PlayMusic(bossMusic);
        
        Destroy(gameObject, 1f);
    }
}
