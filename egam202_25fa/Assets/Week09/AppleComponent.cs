using System.Collections;
using UnityEngine;

public class AppleComponent : MonoBehaviour
{
    public Rigidbody myRb;

    public ShakeAnimator shake;

    public float waitToFallDuration = 3f;
    public float shakeDuration = 1f;
    
    void Start()
    {
        // Make sure to freeze the apple in place
        myRb.isKinematic = true;

        //StartCoroutine(ExecuteFall());
    }

    public IEnumerator ExecuteFall()
    {
        // Wait to fall
        //yield return new WaitForSeconds(waitToFallDuration);

        // SHake to tell players we're about to fall
        shake.Shake();
        yield return new WaitForSeconds(shakeDuration);

        // Finally we can fall
        myRb.isKinematic = false;

        // This is when the coroutine has finished
    }
}
