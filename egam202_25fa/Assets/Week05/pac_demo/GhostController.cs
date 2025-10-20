using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

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

    public Material scatter;
    public Material gohome;

    public float defaultSpeed = 1;
    public float gohomeSpeed = 4;

    public enum MovementState
    {
        Wait,
        Chase,
        Scatter,
        ReturnHome
    }

    MovementState lastMovement;
    public MovementState movement;

    public NavMeshAgent agent;
    public Transform homeTarget;

    PacmanController pacman;
    List<GhostController> ghosts = new List<GhostController>();

    public float gridSize = 1f;

    public List<Transform> patrolHandles;
    public int patrolIndex = 0;
    public float minimumPatrolDistance = 0.1f;

    void Start()
    {
        pacman = FindFirstObjectByType<PacmanController>();

        GhostController[] allGhosts = FindObjectsByType<GhostController>(FindObjectsSortMode.None);
        ghosts.AddRange(allGhosts);

        SetColor();
    }

    // Update is called once per frame
    void Update()
    {
        // DEBUG ONLY - listen for key presses to switch states
        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.qKey.wasPressedThisFrame)
            {
                movement = MovementState.Wait;
            }
            if (keyboard.wKey.wasPressedThisFrame)
            {
                movement = MovementState.Chase;
            }
            if (keyboard.eKey.wasPressedThisFrame)
            {
                movement = MovementState.Scatter;
            }
            if (keyboard.rKey.wasPressedThisFrame)
            {
                movement = MovementState.ReturnHome;
            }
        }

        // See if there's a change in the movement state
        if (movement != lastMovement)
        {
            lastMovement = movement;
            switch (movement)
            {
                case MovementState.Scatter:
                    OnScatterStart();
                    break;
            }
        }

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
        // Update visuals
        SetColor();

        // Update speeds
        agent.speed = defaultSpeed;

        // Warp to the home position
        agent.Warp(homeTarget.position);
    }

    void UpdateChase()
    {
        // Update visuals
        SetColor();

        // Update speeds
        agent.speed = defaultSpeed;

        switch (ghost)
        {
            case GhostType.Red:
                UpdateChaseRed();
                break;
            case GhostType.Pink:
                UpdateChasePink();
                break;
            case GhostType.Blue:
                UpdateChaseBlue();
                break;
        }
    }

    void UpdateChaseRed()
    {
        // Chase pacman directly
        Vector3 pacmanPosition = pacman.transform.position;
        agent.destination = pacmanPosition;
    }

    void UpdateChasePink()
    {
        // Chase two units in front of Pacman
        Vector3 pacmanPosition = pacman.transform.position;
        Vector3 pacmanDirection = pacman.currentDirection;

        Vector3 chaseOffset = pacmanDirection * 2 * gridSize;
        agent.destination = pacmanPosition + chaseOffset;
    }

    void UpdateChaseBlue()
    {
        // Chase the "double" delta from red to pacman
        Vector3 redGhostPosition = Vector3.zero;
        foreach (GhostController ghost in ghosts)
        {
            if (ghost.ghost == GhostType.Red)
            {
                redGhostPosition = ghost.transform.position;
                break;
            }
        }

        // Find the delta from red to pacman (A to B = B - A)
        Vector3 pacmanPosition = pacman.transform.position;
        Vector3 redToPacmanDelta = pacmanPosition - redGhostPosition;

        // Finally create the last position
        agent.destination = pacmanPosition + redToPacmanDelta;
        //redGhostPosition + redToPacmanDelta * 2
    }

    void OnScatterStart()
    {
        int closestIndex = 0;
        float closestDistance = -1;

        // Find the closest poistion in the patrol handle list
        Vector3 ourPosition = transform.position;
        for (int i = 0; i < patrolHandles.Count; i++)        
        {
            Vector3 delta = patrolHandles[i].position - ourPosition;
            float distance = delta.magnitude;

            if (closestDistance == -1 ||
                distance < closestDistance)
            {
                closestIndex = i;
                closestDistance = distance;
            }
        }

        patrolIndex = closestIndex;
    }

    void UpdateScatter()
    {
        // Update visuals
        myRenderer.material = scatter;

        // Update speeds
        agent.speed = defaultSpeed;

        // Move towards out current target
        Transform currentPatrol = patrolHandles[patrolIndex];

        // Move toward the target handle
        Vector3 ourPosition = transform.position;
        Vector3 targetPosition = currentPatrol.position;

        // For A to B = B - A
        Vector3 deltaToTarget = targetPosition - ourPosition;
        deltaToTarget.y = 0;

        agent.destination = targetPosition;

        // Did we get close enough to the handle to pick the next patrol point?
        float deltaDistance = deltaToTarget.magnitude;
        if (deltaDistance < minimumPatrolDistance)
        {
            // Move to the next index
            patrolIndex++;

            // Once we pass the end of the list, reset to the beginning
            if (patrolIndex >= patrolHandles.Count)
            {
                patrolIndex = 0;
            }
        }
    }

    void UpdateReturnHome()
    {
        // Update visuals
        myRenderer.material = gohome;

        // Update speeds
        agent.speed = gohomeSpeed;

        // Go back to the home position
        agent.destination = homeTarget.position;

        Vector3 deltaToTarget = homeTarget.position - transform.position;
        deltaToTarget.y = 0;

        float deltaDistance = deltaToTarget.magnitude;
        if (deltaDistance < minimumPatrolDistance)
        {
            movement = MovementState.Wait;
        }
    }
}
