using UnityEngine;
using TMPro;

public class VectorUi : MonoBehaviour
{
    public Transform handleA;
    public Transform handleB;

    public TextMeshProUGUI text;

    void Update()
    {
        // To find the distance from A to B,
        // the vector math is B - A
        Vector3 posA = handleA.position;
        Vector3 posB = handleB.position;

        Vector3 delta = posB - posA;

        float distance = delta.magnitude;
        text.text = $"Distance between A and B: {distance}";
    }
}
