using UnityEngine;
using UnityEngine.InputSystem;

public class TypingController : MonoBehaviour
{
    void Update()
    {
        // Listen for keyboard input
        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            foreach (var key in keyboard.allKeys)
            {
                if (key != null &&
                    key.wasPressedThisFrame)
                {
                    Debug.Log(key.displayName);
                }
            }
        }

        // Listen for mouse click
        var mouse = Mouse.current;
        if (mouse != null)
        {
            if (mouse.leftButton.wasPressedThisFrame)
            {
                Debug.Log("Left click");
            }
        }
    }
}
