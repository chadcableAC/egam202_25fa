using UnityEngine;
using UnityEngine.InputSystem;

public class RigibodyJumper : MonoBehaviour
{
    public Rigidbody myRb;

    public Vector3 jumpDirection = Vector3.up;
    public float jumpForce = 1;
    public ForceMode jumpMode;

    public float moveStrength = 1;

    InputAction moveAction;
    InputAction jumpAction;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    void Update()
    {
        // Jump action
        if (jumpAction.WasPressedThisFrame())        
        {
            myRb.AddForce(jumpDirection * jumpForce, jumpMode);
        }

        // Joystick input
        Vector2 inputDirection = moveAction.ReadValue<Vector2>();

        // Remap to 3D
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = inputDirection.x;
        moveDirection.z = inputDirection.y;

        // Apply velocity (but maintain the Y value)
        Vector3 newVelocity = moveDirection * moveStrength;
        newVelocity.y = myRb.linearVelocity.y;
        myRb.linearVelocity = newVelocity;
    }

    void FixedUpdate()
    {
        
    }
}
