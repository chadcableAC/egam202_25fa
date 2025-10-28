using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public CinemachineImpulseSource source;

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.wKey.wasPressedThisFrame)
            {
                source.GenerateImpulse();
            }
        }
    }
}
