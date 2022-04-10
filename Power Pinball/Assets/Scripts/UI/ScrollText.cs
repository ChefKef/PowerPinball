using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScrollText : MonoBehaviour
{
    [SerializeField] private RectTransform mask;
    [SerializeField] private RectTransform textBox;
    [SerializeField] private float horizontalScrollSpeed;
    private float maskWidth;
    private float textBoxWidth;
    private float initTextBoxX;
    private bool scrolling;

    // Start is called before the first frame update
    void Start()
    {
        maskWidth = mask.rect.width;
        textBoxWidth = textBox.rect.width;
        initTextBoxX = textBox.position.x;
        scrolling = false;
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

            while (textBox.position.x < initTextBoxX + maskWidth + textBoxWidth)
            {
                textBox.position += new Vector3(horizontalScrollSpeed, 0, 0);
                yield return new WaitForSeconds(0.01f);
            };

            scrolling = false;

            textBox.position = new Vector3(
                initTextBoxX,
                textBox.position.y,
                textBox.position.z);
        }
    }

    public void OnScrollText(InputValue input)
    {
        StartCoroutine(Scroll());
    }
}
