using UnityEngine;

public class MaterialFader : MonoBehaviour
{
    public Renderer myRenderer;

    public float fadeInSeconds = 5f;
    float fadeTimer = 0;

    void Update()
    {
        // Increase timer
        fadeTimer += Time.deltaTime;

        // Remap the timer into an interp (value of 0 to 1)
        float fadeInterp = fadeTimer / fadeInSeconds;

        // Lerp from 1 (full opaque) to 0 (transparent)
        float alpha = Mathf.Lerp(1, 0, fadeInterp);

        // Apply the color
        Color myColor = myRenderer.material.color;
        myColor.a = alpha;
        myRenderer.material.color = myColor;
    }
}
