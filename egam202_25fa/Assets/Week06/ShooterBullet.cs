using UnityEngine;

public class ShooterBullet : MonoBehaviour
{
    public Rigidbody myRb;

    public float speed;

    public float lifetime = 5.0f;
    float timer = 0;

    void Start()
    {
        // Move upware
        myRb.linearVelocity = Vector3.up * speed;
    }

    void Update()
    {
        // Destroy after x seconds
        timer += Time.deltaTime;
        if (timer > lifetime)
        {
            Destroy(gameObject);
        }
    }
}
