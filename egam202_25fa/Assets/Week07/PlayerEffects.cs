using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEffects : MonoBehaviour
{
    public FlashAnimator flash;
    public ShakeAnimator shake;

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.spaceKey.wasPressedThisFrame)
            {
                flash.Flash();
            }

            if (keyboard.qKey.wasPressedThisFrame)
            {
                shake.Shake();
            }
        }
    }
}
