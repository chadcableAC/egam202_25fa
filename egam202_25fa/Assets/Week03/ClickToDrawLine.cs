using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class ClickToDrawLine : MonoBehaviour
{
    public LineRenderer lineRenderer;

    Vector3 firstPoint;

    private void Start()
    {
        lineRenderer.gameObject.SetActive(false);
    }

    void Update()
    {
        var mouse = Mouse.current;
        if (mouse != null)
        {
            // Turn the screen position into a world position
            Vector2 screenPosition = mouse.position.value;
            Ray worldRay = Camera.main.ScreenPointToRay(screenPosition);

            // Click down
            if (mouse.leftButton.wasPressedThisFrame)
            {
                RaycastHit hit;
                if (Physics.Raycast(worldRay, out hit))
                {
                    // Set the first point
                    firstPoint = hit.point;
                }
            }
            // Click held
            else if (mouse.leftButton.isPressed)
            {
                RaycastHit hit;
                if (Physics.Raycast(worldRay, out hit))
                {
                    // Set the "final" point
                    Vector3 finalPoint = hit.point;

                    // Build a list of points
                    List<Vector3> points = new List<Vector3>();
                    points.Add(firstPoint);
                    points.Add(finalPoint);

                    // Set the points to the line renderer
                    // And set the "count" of the line renderer
                    lineRenderer.SetPositions(points.ToArray());
                    lineRenderer.positionCount = points.Count;

                    // Turn the line renderer on
                    lineRenderer.gameObject.SetActive(true);
                }
            }
            // Click released
            else if (mouse.leftButton.wasReleasedThisFrame)
            {
                lineRenderer.gameObject.SetActive(false);
            }

            Debug.DrawRay(worldRay.origin, worldRay.direction * 1000);
        }
    }
}
