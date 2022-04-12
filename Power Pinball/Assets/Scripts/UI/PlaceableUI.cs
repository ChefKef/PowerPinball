using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Defines a UI component that is visible on the gameplay screen.
/// </summary>
public class PlaceableUI : MonoBehaviour
{
    /// <summary>
    /// Parent Canvas object.
    /// </summary>
    [SerializeField] protected Canvas canvas;

    protected TextMeshProUGUI tmp;

    // Thanks to https://answers.unity.com/questions/799616/unity-46-beta-19-how-to-convert-from-world-space-t.html#:~:text=//this%20is%20your,UI_Element.anchoredPosition%3DWorldObject_ScreenPosition%3B
    /// <summary>
    /// Sets the position of the text by projecting from world to canvas space.
    /// </summary>
    /// <param name="worldPos">
    /// Position of object in the world the text should be placed above.
    /// </param>
    /// <param name="tmp">
    /// TextMeshPro object to be positioned.
    /// </param>
    public void SetPosition(Vector2 worldPos)
    {
        canvas = GetComponentInParent<Canvas>();

        // Get the RectTransform component of the Canvas.
        RectTransform CanvasRect = canvas.GetComponent<RectTransform>();

        // Calculate the position of the text.
        //
        // The origin of the Canvas is at the center of the screen, whereas
        // WorldToViewPortPoint() treats the origin as the lower left corner.
        // We need to subtract the height/width of the canvas * 0.5 to get
        // the correct position.
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(worldPos);
        Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * CanvasRect.sizeDelta.x)
                - (CanvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * CanvasRect.sizeDelta.y)
                - (CanvasRect.sizeDelta.y * 0.5f)
                + tmp.rectTransform.rect.height));

        // Used the values obtained to set the position of the UI element.
        tmp.rectTransform.anchoredPosition = WorldObject_ScreenPosition;
    }

    // Thanks to https://answers.unity.com/questions/799616/unity-46-beta-19-how-to-convert-from-world-space-t.html#:~:text=//this%20is%20your,UI_Element.anchoredPosition%3DWorldObject_ScreenPosition%3B
    /// <summary>
    /// Sets the position of the text by projecting from world to canvas space.
    /// </summary>
    /// <param name="worldPos">
    /// Position of object in the world the text should be placed above.
    /// </param>
    /// <param name="go">
    /// RectTransform to be positioned.
    /// </param>
    public void SetPosition(Vector2 worldPos, RectTransform rectTransform)
    {
        canvas = GetComponentInParent<Canvas>();

        // Get the RectTransform component of the Canvas.
        RectTransform CanvasRect = canvas.GetComponent<RectTransform>();

        // Calculate the position of the text.
        //
        // The origin of the Canvas is at the center of the screen, whereas
        // WorldToViewPortPoint() treats the origin as the lower left corner.
        // We need to subtract the height/width of the canvas * 0.5 to get
        // the correct position.
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(worldPos);
        Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * CanvasRect.sizeDelta.x)
                - (CanvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * CanvasRect.sizeDelta.y)
                - (CanvasRect.sizeDelta.y * 0.5f)
                + rectTransform.rect.height));

        // Used the values obtained to set the position of the UI element.
        rectTransform.anchoredPosition = WorldObject_ScreenPosition;
    }
}
