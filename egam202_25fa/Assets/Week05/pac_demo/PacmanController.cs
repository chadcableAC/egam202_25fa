using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PacmanController : MonoBehaviour
{
    InputAction moveAction;

    public NavMeshAgent agent;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }
    
    void Update()
    {
        // Joystick input
        Vector2 inputDirection = moveAction.ReadValue<Vector2>();

        // Remap to 3D
        Vector3 targetOffset = new Vector3(inputDirection.x, 0, inputDirection.y);

        // Set the destination to right in front of pacman
        Vector3 currentPosition = transform.position;
        agent.destination = currentPosition + targetOffset;
    }
}
