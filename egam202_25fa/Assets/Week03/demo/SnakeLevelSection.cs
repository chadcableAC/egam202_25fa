using Unity.Cinemachine;
using UnityEngine;

public class SnakeLevelSection : MonoBehaviour
{
    public CinemachineCamera myCamera;

    private void OnTriggerEnter(Collider other)
    {
        // The "other" is the collider that entered our trigger
        // In this case, the SnakeController.cs is on the collider's parent
        SnakeController snake = other.GetComponentInParent<SnakeController>();
        if (snake)
        {
            // When a snake enters our trigger, make this camera the priority
            myCamera.Prioritize();
        }
    }
}
