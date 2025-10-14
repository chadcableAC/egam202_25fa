using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavController : MonoBehaviour
{
    // High level enemy states
    public EnemyStates currentState;
    public enum EnemyStates
    {
        None,       // 0
        Chase,      // 1
        RunAway,    // 2
        Patrol      // 3
    }

    // Movement values
    public NavMeshAgent agent;
    public Transform targetHandle;

    // High level colors
    public enum EnemyColor
    {
        Red,
        Blue,
        Green
    }

    public Renderer myRenderer;
    public Material materialRed;
    public Material materialBlue;
    public Material materialGreen;

    // Patrol values
    public List<Transform> patrolHandles;
    public int patrolIndex = 0;
    public float minimumPatrolDistance = 0.5f;

    void Start()
    {
        SetColor(EnemyColor.Blue);
    }

    void SetColor(EnemyColor color)
    {
        switch (color)
        {
            case EnemyColor.Red:
                myRenderer.material = materialRed;
                break;

            case EnemyColor.Green:
                myRenderer.material = materialGreen;
                break;

            case EnemyColor.Blue:
                myRenderer.material = materialBlue;
                break;
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyStates.None:
                UpdateNone();
                break;

            case EnemyStates.Chase:
                UpdateChase();
                break;

            case EnemyStates.RunAway:
                UpdateRunAway();
                break;

            case EnemyStates.Patrol:
                UpdatePatrol();
                break;
        }
    }

    void UpdateNone()
    {
        SetColor(EnemyColor.Blue);

        // Stop moving
        agent.isStopped = true;
    }

    void UpdateChase()
    {
        SetColor(EnemyColor.Red);

        // Start moving
        agent.isStopped = false;

        // Go to target
        Vector3 targetPosition = targetHandle.position;
        agent.destination = targetPosition;
    }

    void UpdateRunAway()
    {
        SetColor(EnemyColor.Green);

        // Start moving
        agent.isStopped = false;

        // Move toward the target handle
        Vector3 ourPosition = transform.position;
        Vector3 targetPosition = targetHandle.position;

        // For A to B = B - A
        Vector3 deltaToTarget = targetPosition - ourPosition;
        Vector3 deltaDirection = deltaToTarget.normalized;
        Vector3 deltaOpposite = deltaDirection * -1;

        // Come up with a new position to escape to
        Vector3 escapePoint = ourPosition + deltaOpposite;
        agent.destination = escapePoint;
    }

    void UpdatePatrol()
    {
        SetColor(EnemyColor.Blue);

        // Move towards out current target
        Transform currentPatrol = patrolHandles[patrolIndex];

        // Move toward the target handle
        Vector3 ourPosition = transform.position;
        Vector3 targetPosition = currentPatrol.position;

        // For A to B = B - A
        Vector3 deltaToTarget = targetPosition - ourPosition;
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
}
