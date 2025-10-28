using UnityEngine;

public class ShakeAnimator : MonoBehaviour
{
    // Transform to shake
    public Transform shakeHandle;
    Vector3 originalPosition;

    // Strength of shake in each direction
    public Vector3 shakeStrengths;
    public AnimationCurve strengthCurve;

    // Timing
    public float duration = 1;
    float timer = -1;

    public float perShakeDuration = 0.1f;
    float perShakeTimer = -1;

    void Update()
    {
        // Update the timer when >= to 0
        if (timer >= 0)
        {
            timer += Time.deltaTime;

            // Over the duration? Reset
            if (timer >= duration)
            {
                timer = -1;
                ResetShake();                
            }
            else
            {
                // Add to the "per shake" timer
                // This prevents us shaking every frame
                perShakeTimer += Time.deltaTime;
                if (perShakeTimer > perShakeDuration)
                {
                    perShakeTimer = 0;
                    ApplyShake();
                }
            }
        }
    }

    public void Shake()
    {
        // Reset timers
        timer = 0;
        perShakeTimer = 0;

        // Remember the current position
        originalPosition = transform.position;
    }

    void ApplyShake()
    {
        Vector3 offset = Vector3.zero;

        // Randomize the offset
        offset.x = shakeStrengths.x * Random.Range(-1, 1f);
        offset.y = shakeStrengths.y * Random.Range(-1, 1f);
        offset.z = shakeStrengths.z * Random.Range(-1, 1f);

        // Use the curve to adjust the strenth
        float interp = timer / duration;
        float animInterp = strengthCurve.Evaluate(interp);
        offset *= animInterp;

        // Set the final position
        transform.position = originalPosition + offset;
    }

    void ResetShake()
    {
        transform.position = originalPosition;
    }
}
