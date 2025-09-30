using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public Renderer myRenderer;

    public void OnClick()
    {
        myRenderer.material.color = Color.blue;
    }
}
