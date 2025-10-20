using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PacmanController : MonoBehaviour
{
    InputAction moveAction;

    public NavMeshAgent agent;

    public Vector3 currentDirection;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }
    
    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.spaceKey.wasPressedThisFrame)
            {
                ScorePopupManager score = FindFirstObjectByType<ScorePopupManager>();
                if (score != null)
                {
                    score.PlayPopup(transform.position, 200);
                }
            }
        }


        // Joystick input
        Vector2 inputDirection = moveAction.ReadValue<Vector2>();

        // Remap to 3D
        Vector3 targetOffset = new Vector3(inputDirection.x, 0, inputDirection.y);
        if (targetOffset.magnitude > 0)
        {
            currentDirection = targetOffset;
        }        

        // Set the destination to right in front of pacman
        Vector3 currentPosition = transform.position;
        agent.destination = currentPosition + targetOffset;
    }
}
