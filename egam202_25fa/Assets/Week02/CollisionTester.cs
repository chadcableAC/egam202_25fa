using UnityEngine;

public class CollisionTester : MonoBehaviour
{
    public Renderer myRenderer;

    private void OnCollisionEnter(Collision collision)
    {
        myRenderer.material.color = Color.green;
    }

    private void OnCollisionExit(Collision collision)
    {
        myRenderer.material.color = Color.red;
    }

    private void OnCollisionStay(Collision collision)
    {

    }
}
