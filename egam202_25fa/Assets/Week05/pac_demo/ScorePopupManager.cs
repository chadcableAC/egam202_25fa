using UnityEngine;

public class ScorePopupManager : MonoBehaviour
{
    public ScorePopupUi popupPrefab;

    public void PlayPopup(Vector3 worldPosition, int score)
    {
        // First let's make a new popup
        ScorePopupUi popup = Instantiate(popupPrefab);
        popup.transform.SetParent(transform);
        popup.Play(score);

        // Turn the 3d world position into screen position (pixels)
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        // Turning the screen position into a local position (using our parent)
        Vector3 uiPosition = transform.InverseTransformPoint(screenPosition);

        popup.transform.localPosition = uiPosition;
    }
}
