using UnityEngine;

public class HoverMoveUp : MonoBehaviour
{
    private Vector3 originalPosition;
    public float hoverHeight = 0.5f;

    void Start()
    {
        originalPosition = transform.position;
    }

    void OnMouseEnter()
    {
        transform.position = originalPosition + Vector3.up * hoverHeight;
    }

    void OnMouseExit()
    {
        transform.position = originalPosition;
    }
}