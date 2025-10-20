using UnityEngine;
using UnityEngine.InputSystem;

public class ShooterController : MonoBehaviour
{
    public Rigidbody myRb;

    public float moveSpeed = 1;

    InputAction moveAction;
    InputAction fireAction;

    public Transform spawnHandle;
    public ShooterBullet bulletPrefab;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        fireAction = InputSystem.actions.FindAction("Jump");
    }

    void Update()
    {
        // Joystick input
        Vector2 inputDirection = moveAction.ReadValue<Vector2>();
        myRb.linearVelocity = Vector3.right * inputDirection.x * moveSpeed;

        // Fire action
        if (fireAction.WasPerformedThisFrame())
        {
            ShooterBullet bullet = Instantiate(bulletPrefab);
            bullet.transform.position = spawnHandle.position;
        }
    }
}
