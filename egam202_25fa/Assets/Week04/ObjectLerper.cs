using UnityEngine;

public class ObjectLerper : MonoBehaviour
{
    public Transform handleA;
    public Transform handleB;

    public float interpValue = 0;

    Color colorA;
    Color colorB;
    public Renderer myRenderer;

    private void Start()
    {
        Renderer rendererA = handleA.GetComponent<Renderer>();
        if (rendererA)
        {
            colorA = rendererA.material.color;
        }

        Renderer rendererB = handleB.GetComponent<Renderer>();
        if (rendererB)
        {
            colorB = rendererB.material.color;
        }
    }

    void Update()
    {
        Vector3 posA = handleA.position;
        Vector3 posB = handleB.position;

        //transform.position = Vector3.Lerp(posA, posB, interpValue);
        transform.position = Vector3.LerpUnclamped(posA, posB, interpValue);

        Color lerpColor = Color.Lerp(colorA, colorB, interpValue);
        myRenderer.material.color = lerpColor;
    }
}
