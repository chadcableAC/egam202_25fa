using UnityEngine;
using UnityEngine.AI;

public class NavPlayerController : MonoBehaviour
{
    public NavMeshAgent navAgent;

    public Transform chaseHandle;

    void Update()
    {
        navAgent.destination = chaseHandle.position;
    }
}
