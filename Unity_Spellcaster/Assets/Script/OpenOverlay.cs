using UnityEngine;

public class OpenOverlay : MonoBehaviour
{
    // Drag the overlay GameObject here in the Inspector
    public GameObject overlayLayer;

    // Call this method to toggle the overlay on/off
    public void ToggleOverlay()
    {
        if (overlayLayer != null)
        {
            overlayLayer.SetActive(!overlayLayer.activeSelf);
        }
    }
}
