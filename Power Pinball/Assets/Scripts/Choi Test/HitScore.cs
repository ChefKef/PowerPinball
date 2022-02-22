using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitScore : MonoBehaviour
{
    //   Vector2 pos = gameObject.transform.position;  // get the game object position
    //   Vector2 viewportPoint = Camera.main.WorldToViewportPoint(pos);  //convert game object position to VievportPoint

    //   // set MIN and MAX Anchor values(positions) to the same position (ViewportPoint)
    //   rectTransform.anchorMin = viewportPoint;  
    //rectTransform.anchorMax = viewportPoint; 



    

    private TextMeshProUGUI tmp;
    [SerializeField] Canvas canvas;
    
    public void SetText(string text)
    {
        tmp.text = text;
    }

    public void SetPosition(Vector2 worldPos)
    {
        //// Convert game object position to VievportPoint
        //Vector2 viewportPoint = Camera.main.WorldToViewportPoint(worldPos);

        //// set MIN and MAX Anchor values(positions) to the same position (ViewportPoint)
        //tmp.rectTransform.anchorMin = viewportPoint;
        //tmp.rectTransform.anchorMax = viewportPoint;

        //tmp.rectTransform.position = new Vector2(x, y);



        canvas = GetComponentInParent<Canvas>();

        //this is your object that you want to have the UI element hovering over
        GameObject WorldObject;

        //this is the ui element
        RectTransform UI_Element;

        //first you need the RectTransform component of your canvas
        RectTransform CanvasRect = canvas.GetComponent<RectTransform>();

        //then you calculate the position of the UI element
        //0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.

        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(worldPos);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

        //now you can set the position of the ui element
        tmp.rectTransform.anchoredPosition = WorldObject_ScreenPosition;
    }

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
            tmp.color.a - 0.01f); // Only modify alpha channel.
    }

    // Start is called before the first frame update
    void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // Fade out if not completely transparent yet. Otherwise, get rid of
        // the object since it is no longer needed.
        if (tmp.color.a > 0) Fade();
        else gameObject.SetActive(false);
    }
}
