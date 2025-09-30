using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenRelativeController : MonoBehaviour
{
    public Rigidbody myRigidbody;
    public float movementStrength = 1;

    InputAction moveAction;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        // Read the input
        Vector2 inputDirection = moveAction.ReadValue<Vector2>();

        // Turn the screen direction into world directions
        Transform cameraHandle = Camera.main.transform;

        Vector3 worldForward = cameraHandle.forward;
        worldForward.y = 0;
        worldForward.Normalize();

        Vector3 worldRight = cameraHandle.right;
        worldRight.y = 0;
        worldRight.Normalize();

        // Set speed based on the current input
        Vector3 moveDirection = Vector3.zero;
        moveDirection += worldRight * inputDirection.x;
        moveDirection += worldForward * inputDirection.y;

        // Ensure this remains 1 unit long
        if (moveDirection.magnitude > 1)
        {
            moveDirection.Normalize();
        }

        // Keep the rigidbody's y velocity (so that we're not resetting it to 0 every frame)
        Vector3 finalVelocity = moveDirection * movementStrength;
        finalVelocity.y = myRigidbody.linearVelocity.y;
        myRigidbody.linearVelocity = finalVelocity;
    }
}
