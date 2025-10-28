using UnityEngine;

public class FlashAnimator : MonoBehaviour
{
    public Renderer myRenderer;

    public float duration = 1f;
    float timer = -1;

    public AnimationCurve curve = null;

    void Start()
    {
        // We want the flash to dissapear
        SetInterp(1);
    }

    void Update()
    {
        // Update the timer when >= 0
        if (timer >= 0)
        {
            timer += Time.deltaTime;

            // Over the duration? Stop the effect
            if (timer >= duration)
            {
                timer = -1;
                SetInterp(1);
            }
            else
            {
                float interp = timer / duration;
                SetInterp(interp);
            }
        }
    }

    public void Flash()
    {
        // Start the timers
        timer = 0;
    }

    void SetInterp(float interp)
    {
        // Reimagine the interp as a value on this curve
        float animInterp = curve.Evaluate(interp);

        float alpha = Mathf.Lerp(1, 0, animInterp);

        Color color = myRenderer.material.color;
        color.a = alpha;
        myRenderer.material.color = color;
    }
}
