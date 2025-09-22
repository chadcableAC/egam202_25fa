using UnityEngine;

public class AppleCollectible : MonoBehaviour
{
    public void Collect()
    {
        // Destroy ourselves
        Destroy(gameObject);
    }
}
