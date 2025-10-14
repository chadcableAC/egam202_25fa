using UnityEngine;
using UnityEngine.AI;

public class GhostController : MonoBehaviour
{
    public enum GhostType
    {
        Red,
        Pink,
        Blue,
        Orange
    }

    public GhostType ghost;

    public Renderer myRenderer;
    public Material red;
    public Material pink;
    public Material blue;
    public Material orange;

    public enum MovementState
    {
        Wait,
        Chase,
        Scatter,
        ReturnHome
    }

    public MovementState movement;

    public NavMeshAgent agent;
    public Transform homeTarget;
    PacmanController pacman;

    void Start()
    {
        pacman = FindFirstObjectByType<PacmanController>();

        SetColor();
    }

    // Update is called once per frame
    void Update()
    {
        switch (movement)
        {
            case MovementState.Wait:
                UpdateWait();
                break;
            case MovementState.Chase:
                UpdateChase();
                break;
            case MovementState.Scatter:
                UpdateScatter();
                break;
            case MovementState.ReturnHome:
                UpdateReturnHome();
                break;
        }
    }

    void SetColor()
    {
        switch (ghost)
        {
            case GhostType.Red:
                myRenderer.material = red;
                break;
            case GhostType.Blue:
                myRenderer.material = blue;
                break;
            case GhostType.Pink:
                myRenderer.material = pink;
                break;
            case GhostType.Orange:
                myRenderer.material = orange;
                break;
        }
    }

    void UpdateWait()
    {
        // Warp to the home position
        agent.Warp(homeTarget.position);

        // Reset (clear) the path
        agent.ResetPath();
    }

    void UpdateChase()
    {
        switch (ghost)
        {
            case GhostType.Red:
                UpdateChaseRed();
                break;
        }
    }

    void UpdateChaseRed()
    {
        // Chase pacman directly
        Vector3 pacmanPosition = pacman.transform.position;
        agent.destination = pacmanPosition;
    }

    void UpdateScatter()
    {

    }

    void UpdateReturnHome()
    {
        // Go back to the home position
        agent.destination = homeTarget.position;
    }
}
