using UnityEngine;
using TMPro;

public class SnakeGameUi : MonoBehaviour
{
    public TextMeshProUGUI scoreCounter;
    public GameObject loseHandle;
    SnakeController snake;

    void Start()
    {
        // Turn off the lose
        loseHandle.SetActive(false);

        // Look for the snake object
        snake = FindAnyObjectByType<SnakeController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (snake != null)
        {
            // Set the counter
            int apples = snake.applesEaten;
            scoreCounter.text = $"Apples: {apples}";
        }
        else
        {
            // No snake = game over
            loseHandle.SetActive(true);
        }
    }
}
