using UnityEngine;
using UnityEngine.InputSystem;

public class MaterialChanger : MonoBehaviour
{
    public Renderer meshRenderer;

    public Material material0;
    public Material material1;

    void Start()
    {
        meshRenderer.material = material0;
    }

    // Update is called once per frame
    void Update()
    {
        // On mouse, toggle the material
        var mouse = Mouse.current;
        if (mouse != null)
        {
            if (mouse.leftButton.wasPressedThisFrame)
            {
                // We need to use sharedMaterial to evaluate
                if (meshRenderer.sharedMaterial == material0)
                {
                    Debug.Log("Go to 1");
                    meshRenderer.material = material1;
                }
                else
                {
                    Debug.Log("Go to 0");
                    meshRenderer.material = material0;
                }
            }
        }
    }
}
