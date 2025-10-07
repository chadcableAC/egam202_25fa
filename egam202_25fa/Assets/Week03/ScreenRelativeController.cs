using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenRelativeController : MonoBehaviour
{
    public Rigidbody myRigidbody;
    public float movementStrength = 1;

    InputAction moveAction;

    public bool isRelative = true;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        if (isRelative)
        {
            UpdateRelative();
        }
        else
        {
            UpdateAbsolute();
        }
    }

    void UpdateRelative()
    { 
        // Read the input
        Vector2 inputDirection = moveAction.ReadValue<Vector2>();

        // Turn the screen direction into world directions
        Transform cameraHandle = Camera.main.transform;

        Vector3 cameraRight = cameraHandle.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        Vector3 cameraForward = cameraHandle.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        // Set speed based on the current input
        Vector3 moveDirection = Vector3.zero;
        moveDirection += cameraRight * inputDirection.x;
        moveDirection += cameraForward * inputDirection.y;

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

    void UpdateAbsolute()
    {
        // Read the input
        Vector2 inputDirection = moveAction.ReadValue<Vector2>();

        // Set speed based on the current input
        Vector3 moveDirection = Vector3.zero;
        moveDirection += Vector3.right * inputDirection.x;
        moveDirection += Vector3.forward * inputDirection.y;

        // Keep the rigidbody's y velocity (so that we're not resetting it to 0 every frame)
        Vector3 finalVelocity = moveDirection * movementStrength;
        finalVelocity.y = myRigidbody.linearVelocity.y;
        myRigidbody.linearVelocity = finalVelocity;
    }
}
