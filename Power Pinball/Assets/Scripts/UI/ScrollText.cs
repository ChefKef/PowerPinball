using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScrollText : PlaceableUI
{
    /// <summary>
    /// Mask parent GameObject.
    /// </summary>
    [SerializeField] private RectTransform mask;

    /// <summary>
    /// The actual text to scroll.
    /// </summary>
    [SerializeField] private RectTransform textBox;

    [SerializeField] private float horizontalScrollSpeed;

    /// <summary>
    /// Rectangular sprite on the game board representing where to overlay the
    /// mask on the UI canvas.
    /// </summary>
    [SerializeField] private GameObject refRect;

    private float maskWidth;
    private float textBoxWidth;

    /// <summary>
    /// Initial position of the text, as positioned using the Editor.
    /// </summary>
    private float initTextBoxX;

    /// <summary>
    /// Flag used to prevent the coroutine from being called while it is playing.
    /// </summary>
    private bool scrolling;

    // Start is called before the first frame update
    void Start()
    {
        maskWidth = mask.rect.width;
        textBoxWidth = textBox.rect.width;
        initTextBoxX = textBox.position.x;
        scrolling = false;
        //SetPosition(refRect.transform.position, mask);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetAxis("Submit") > 0) StartCoroutine(Scroll());
    }

    private IEnumerator Scroll()
    {
        if (!scrolling)
        {
            scrolling = true;

            // Scroll text R to L until it is out of view again.
            while (textBox.position.x > initTextBoxX - 2.2f * (maskWidth + textBoxWidth))
            {
                textBox.position -= new Vector3(horizontalScrollSpeed, 0, 0);
                yield return new WaitForSeconds(0.01f);
            };

            scrolling = false;

            // Reset text location for next scroll.
            textBox.position = new Vector3(
                initTextBoxX,
                textBox.position.y,
                textBox.position.z);
        }
    }

    /// <summary>
    /// Listens for the ENTER keypress.
    /// </summary>
    /// <param name="input"></param>
    public void OnScrollText(InputValue input)
    {
        StartCoroutine(Scroll());
    }
}
