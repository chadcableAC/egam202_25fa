using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class HighScoreUi : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    int score = 0;
    int hiScore = 0;

    string scoreKey = "score";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hiScore = PlayerPrefs.GetInt(scoreKey, 0);
    }

    // Update is called once per frame
    void Update()
    {
        var mouse = Mouse.current;
        if (mouse != null)
        {
            if (mouse.leftButton.wasPressedThisFrame)
            {
                score += 1;
                if (score > hiScore)
                {
                    PlayerPrefs.SetInt(scoreKey, score);
                    hiScore = score;
                }
            }
            else if (mouse.rightButton.wasPressedThisFrame)
            {
                score -= 1;
            }
        }

        scoreText.text = $"Score: {score}, hi score {hiScore}";
    }
}
