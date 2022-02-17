using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    /// <summary>
    /// How far to push the TMP rectangle right.
    /// </summary>
    [SerializeField]
    private int leftMargin;

    /// <summary>
    /// How far to push the TMP rectangle up.
    /// </summary>
    [SerializeField]
    private int bottomMargin;

    // Start is called before the first frame update
    void Start()
    {
        // Leverage width and height of the TMP's rectangle to ensure it fits
        // wholly onscreen.
        scoreText.rectTransform.position = new Vector3(
            scoreText.rectTransform.rect.width + leftMargin, 
            scoreText.rectTransform.rect.height + bottomMargin);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + GameManager.scoreP1;
    }
}
