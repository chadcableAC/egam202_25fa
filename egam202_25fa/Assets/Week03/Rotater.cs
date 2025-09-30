using UnityEngine;

public class Rotater : MonoBehaviour
{
    public float degreesPerSecond = 1f;

    void Update()
    {
        float degreesThisFrame = degreesPerSecond * Time.deltaTime;
        Quaternion offset = Quaternion.Euler(Vector3.up * degreesThisFrame);
        transform.localRotation *= offset;
    }
}
