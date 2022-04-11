using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboCounter : PlaceableUI
{
    ///// <summary>
    ///// Speed at which text fades.
    ///// </summary>
    //[SerializeField] private float fadeSpeed;

    ///// <summary>
    ///// Speed at which text "wipes" upward.
    ///// </summary>
    //[SerializeField] private float moveUpSpeed;
    
    public void SetText(int hits)
    {
        // Deal with singular vs plural shenanigans.
        if (hits == 1) tmp.text = hits.ToString() + " Hit!";
        else tmp.text = hits.ToString() + " Hits!";
    }

    ///// <summary>
    ///// Return the alpha value to 1. Required, since the active state of the 
    ///// object is dependent on the alpha value.
    ///// </summary>
    //public void ResetAlpha()
    //{
    //    tmp.color = new Color(
    //        tmp.color.r,
    //        tmp.color.g,
    //        tmp.color.b,
    //        1); // Only modify alpha channel.
    //}

    ///// <summary>
    ///// Gradually decreases TMPro transparency to "fade out" the text.
    ///// </summary>
    //private void Fade()
    //{
    //    tmp.color = new Color(
    //        tmp.color.r,
    //        tmp.color.g,
    //        tmp.color.b,
    //        tmp.color.a - fadeSpeed); // Only modify alpha channel.
    //}

    ///// <summary>
    ///// Gradually decreases TMPro position to "wipe" the text upwards.
    ///// </summary>
    //private void MoveUp()
    //{
    //    tmp.rectTransform.position = new Vector2(
    //        tmp.rectTransform.position.x,
    //        tmp.rectTransform.position.y + moveUpSpeed);
    //}

    // Start is called before the first frame update
    void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //// Fade out and wipe up if not completely transparent yet. Otherwise,
        //// return the object to the pool it is no longer needed.
        //if (tmp.color.a > 0)
        //{
        //    Fade();
        //    MoveUp();
        //}
        //else gameObject.SetActive(false);
    }
}
