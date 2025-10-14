using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
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
    public Rigidbody myRb;
    public Transform targetHandle;
    public float moveSpeed = 5f;

    // High level colors
    public enum EnemyColor
    {
        Red,
        Blue,
        Green
    }

    //public EnemyColor currentColor;

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

        // Nothing to do, reset our velocity
        myRb.linearVelocity = Vector3.zero;
    }

    void UpdateChase()
    {
        SetColor(EnemyColor.Red);

        // Move toward the target handle
        Vector3 ourPosition = transform.position;
        Vector3 targetPosition = targetHandle.position;

        // For A to B = B - A
        Vector3 deltaToTarget = targetPosition - ourPosition;
        Vector3 deltaDirection = deltaToTarget.normalized;

        // Directly assign the velocity
        myRb.linearVelocity = deltaDirection * moveSpeed;
    }

    void UpdateRunAway()
    {
        SetColor(EnemyColor.Green);

        // Move toward the target handle
        Vector3 ourPosition = transform.position;
        Vector3 targetPosition = targetHandle.position;

        // For A to B = B - A
        Vector3 deltaToTarget = targetPosition - ourPosition;
        Vector3 deltaDirection = deltaToTarget.normalized;
        Vector3 deltaOpposite = deltaDirection * -1;

        // Directly assign the velocity
        myRb.linearVelocity = deltaOpposite * moveSpeed;
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
        Vector3 deltaDirection = deltaToTarget.normalized;

        // Directly assign the velocity
        myRb.linearVelocity = deltaDirection * moveSpeed;

        // Did we get close enough to the handle to pick the next patrol point?
        float deltaDistance = deltaToTarget.magnitude;
        if (deltaDistance < minimumPatrolDistance)
        {
            // These three lines all do the same thing
            patrolIndex++;
            //patrolIndex += 1;
            //patrolIndex = patrolIndex + 1;

            // Once we've passed the end of the list,
            // loop back to the beginning
            if (patrolIndex >= patrolHandles.Count)
            {
                patrolIndex = 0;
            }
        }
    }
}
