using UnityEngine;
using UnityEngine.InputSystem;

public class PopManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var mouse = Mouse.current;
        if (mouse != null)
        {
            // Turn the screen position
            Vector2 screenPosition = mouse.position.value;

            // Into a "world" position
            Ray worldRay = Camera.main.ScreenPointToRay(screenPosition);

            // On a mouse click...
            if (mouse.leftButton.wasPressedThisFrame)
            {
                // Send a "raycast" to see what we hit / collide with
                RaycastHit hit;
                if (Physics.Raycast(worldRay, out hit))
                {
                    // Is this object a ClickableObject?
                    PopObject pop = hit.transform.GetComponent<PopObject>();
                    if (pop)
                    {
                        pop.Popped();
                    }
                }
            }

            Debug.DrawRay(worldRay.origin, worldRay.direction * 1000);
        }
    }
}
