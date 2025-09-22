using UnityEngine;

public class TriggerTester : MonoBehaviour
{
    public Renderer myRenderer;

    private void OnTriggerEnter(Collider other)
    {
        myRenderer.material.color = Color.green;
    }

    private void OnTriggerExit(Collider other)
    {
        myRenderer.material.color = Color.red;
    }

    private void OnTriggerStay(Collider other)
    {

    }
}
