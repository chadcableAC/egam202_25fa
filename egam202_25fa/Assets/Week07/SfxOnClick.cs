using UnityEngine;
using UnityEngine.InputSystem;

public class SfxOnClick : MonoBehaviour
{
    // Audio source
    public AudioSource sfx;

    // Possible SFX clips
    public AudioClip[] clips;

    void Update()
    {
        var mouse = Mouse.current;
        if (mouse != null)
        {
            if (mouse.leftButton.wasPressedThisFrame)
            {
                // Pick a random clip to play
                int randomIndex = Random.Range(0, clips.Length - 1);
                sfx.clip = clips[randomIndex];
                sfx.Play();
            }
        }
    }
}
