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
            // Listen for a specific key (Like A)
            if (keyboard.aKey.wasPressedThisFrame)
            {
                Debug.Log("A pressed!");
            }

            var aa = keyboard.aKey.wasReleasedThisFrame;
            var aaa = keyboard.aKey.isPressed;

            // Listen for ANY key
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
