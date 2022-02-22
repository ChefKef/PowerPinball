using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitScore : MonoBehaviour
{
    private TextMeshProUGUI tmp;

    public bool Enabled
    {
        get { return tmp.enabled; }
        set { tmp.enabled = value; }
    }
    
    public void SetText(string text)
    {
        tmp.text = text;
    }

    public void SetPosition(float x, float y)
    {
        tmp.rectTransform.position = new Vector2(x, y);
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
        else tmp.enabled = false;
    }
}
