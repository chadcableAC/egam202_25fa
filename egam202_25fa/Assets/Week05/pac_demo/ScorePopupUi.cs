using TMPro;
using UnityEngine;

public class ScorePopupUi : MonoBehaviour
{
    public TextMeshProUGUI textUi;

    public float duration = 3;
    float timer = 0;

    public void Play(int score)
    {
        textUi.text = $"+{score}";
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > duration)
        {
            Destroy(gameObject);
        }
    }
}
