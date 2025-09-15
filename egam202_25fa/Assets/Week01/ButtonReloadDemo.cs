using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonReloadDemo : MonoBehaviour
{
    public void OnReload()
    {
        // Get the current scene's build index
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Reload the scene
        SceneManager.LoadScene(sceneIndex);
    }
}
