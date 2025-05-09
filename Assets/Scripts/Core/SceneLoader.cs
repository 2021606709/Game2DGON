using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    /**
     * This script is used to load a scene in Unity.
     * It uses the SceneManager class to load the scene by name.
     * The scene name is passed as a parameter to the LoadScene method.
     * The QuitGame method is used to exit the game.
     */
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public static void QuitGame()
    {
         #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
