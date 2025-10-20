using UnityEngine;

public class SpaceEnemyController : MonoBehaviour
{
    public Rigidbody myRb;

    private void OnTriggerEnter(Collider other)
    {
        // Did we get hit by a bullet? Delete both
        ShooterBullet bullet = other.transform.GetComponent<ShooterBullet>();
        if (bullet != null)
        {
            Destroy(gameObject);
            Destroy(bullet.gameObject);
        }
    }
}
