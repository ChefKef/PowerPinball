using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitScore : MonoBehaviour
{
    /// <summary>
    /// Speed at which text fades.
    /// </summary>
    [SerializeField] private float fadeSpeed;

    /// <summary>
    /// Speed at which text "wipes" upward.
    /// </summary>
    [SerializeField] private float moveUpSpeed;

    /// <summary>
    /// Parent Canvas object.
    /// </summary>
    [SerializeField] Canvas canvas;

    private TextMeshProUGUI tmp;
    
    public void SetText(string text)
    {
        tmp.text = text;
    }

    // Thanks to https://answers.unity.com/questions/799616/unity-46-beta-19-how-to-convert-from-world-space-t.html#:~:text=//this%20is%20your,UI_Element.anchoredPosition%3DWorldObject_ScreenPosition%3B
    /// <summary>
    /// Sets the position of the text by projecting from world to canvas space.
    /// </summary>
    /// <param name="worldPos">
    /// Position of object in the world the text should be placed above.
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

    /// <summary>
    /// Return the alpha value to 1. Required, since the active state of the 
    /// object is dependent on the alpha value.
    /// </summary>
    public void ResetAlpha()
    {
        tmp.color = new Color(
            tmp.color.r,
            tmp.color.g,
            tmp.color.b,
            1); // Only modify alpha channel.
    }

    /// <summary>
    /// Gradually decreases TMPro transparency to "fade out" the text.
    /// </summary>
    private void Fade()
    {
        tmp.color = new Color(
            tmp.color.r,
            tmp.color.g,
            tmp.color.b,
            tmp.color.a - fadeSpeed); // Only modify alpha channel.
    }

    /// <summary>
    /// Gradually decreases TMPro position to "wipe" the text upwards.
    /// </summary>
    private void MoveUp()
    {
        tmp.rectTransform.position = new Vector2(
            tmp.rectTransform.position.x,
            tmp.rectTransform.position.y + moveUpSpeed);
    }

    // Start is called before the first frame update
    void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // Fade out and wipe up if not completely transparent yet. Otherwise,
        // return the object to the pool it is no longer needed.
        if (tmp.color.a > 0)
        {
            Fade();
            MoveUp();
        }
        else gameObject.SetActive(false);
    }
}
