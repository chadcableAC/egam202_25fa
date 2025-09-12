using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonReload : MonoBehaviour
{
    public void OnReload()
    {
        // Get the current scene index
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
