using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeController : MonoBehaviour
{
    // Rigidbody
    public Rigidbody myRb;

    // Movement values
    InputAction moveAction;
    public float moveStrength = 1;
    Vector3 lastDirection = Vector3.forward;

    // Apple counter
    public int applesEaten = 0;

    // Visuals
    public Transform visualHandle;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void FixedUpdate()
    {
        // Read the joystick
        Vector2 inputDirection = moveAction.ReadValue<Vector2>();

        // Remap the directions: left/right = x, forward/back = y
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = inputDirection.x;
        moveDirection.z = inputDirection.y;

        // Only move in grid / cardinal directions (No diagonals)
        if (moveDirection.x != 0)
        {
            lastDirection.x = moveDirection.x;
            lastDirection.z = 0;
        }
        else if (moveDirection.z != 0)
        {
            lastDirection.x = 0;
            lastDirection.z = moveDirection.z;
        }

        // Final velocity (Uses lastDirection to keep the snake moving)
        Vector3 newVelocity = lastDirection * moveStrength;
        // Maintain our y velocity
        newVelocity.y = myRb.linearVelocity.y;
        // Finally assign
        myRb.linearVelocity = newVelocity;


        // Rotate the visuals to the direction
        visualHandle.forward = lastDirection;
    }

    private void OnTriggerEnter(Collider other)
    {
        AppleCollectible apple = other.transform.GetComponent<AppleCollectible>();
        if (apple != null)
        {
            // Add one to score, delete the apple
            applesEaten += 1;
            apple.Collect();
        }

        ObstacleObject obstacle = other.transform.GetComponent<ObstacleObject>();
        if (obstacle != null)
        {
            // Destroy ourselves
            Destroy(gameObject);
        }
    }
}
