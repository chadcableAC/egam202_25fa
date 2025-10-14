using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebakeNav : MonoBehaviour
{
    public NavMeshSurface surface;

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.aKey.wasPressedThisFrame)
            {
                surface.BuildNavMesh();
            }
        }
    }
}
