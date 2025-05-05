using UnityEngine;

public class OpenCloseOverlay : MonoBehaviour
{
    public GameObject layerA; // Assign in Inspector
    public GameObject layerB; // Assign in Inspector

    private bool isLayerAActive = true;

    public void ToggleLayers()
    {
        isLayerAActive = !isLayerAActive;

        layerA.SetActive(isLayerAActive);
        layerB.SetActive(!isLayerAActive);
    }
}
