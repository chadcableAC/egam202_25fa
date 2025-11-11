using System.Collections;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    public float betweenFallDuration = 1f;

    void Start()
    {
        StartCoroutine(ExecuteTree());
    }

    public IEnumerator ExecuteTree()
    {
        AppleComponent[] apples = transform.GetComponentsInChildren<AppleComponent>();

        // Go through the apples, and make each one fall
        foreach (AppleComponent apple in apples)
        {
            // Wait until the ExecuteFall coroutine has finished
            yield return StartCoroutine(apple.ExecuteFall());


            //yield return new WaitForSeconds(betweenFallDuration);            
        }
    }
}
