using UnityEngine;
using UnityEngine.InputSystem;

public class ParticleOnClick : MonoBehaviour
{
    public ParticleSystem fx;

    void Update()
    {
        var mouse = Mouse.current;
        if (mouse != null)
        {
            if (mouse.leftButton.wasPressedThisFrame)
            {
                fx.Play();
            }
        }
    }
}
