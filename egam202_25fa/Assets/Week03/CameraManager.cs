using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    public List<CinemachineCamera> cameras;
    public int cameraIndex = 0;

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.leftArrowKey.wasPressedThisFrame)
            {
                // Go down one index
                int newIndex = cameraIndex - 1;

                // Less than zero? Loop to the back of the list
                if (newIndex < 0)
                {
                    // Remember lists are "off by one"
                    newIndex = cameras.Count - 1;
                }

                SetCamera(newIndex);
            }

            if (keyboard.rightArrowKey.wasPressedThisFrame)
            {
                // Go up one index
                int newIndex = cameraIndex + 1;

                // Bigger than the list? Loop to the beginning of the list
                if (newIndex >= cameras.Count)
                {
                    newIndex = 0;
                }

                SetCamera(newIndex);
            }
        }
    }

    public void SetCamera(int index)
    {
        // Make this camera the priority
        cameras[index].Prioritize();
    }
}
