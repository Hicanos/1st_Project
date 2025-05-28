using UnityEngine;

public class VisibilityFilter : MonoBehaviour
{
    public Color reactionColor;
    [HideInInspector]
    public Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.enabled = false;
    }

    public bool IsLightColorMatching(Color lightColor)
    {
        return lightColor == reactionColor;
    }
}