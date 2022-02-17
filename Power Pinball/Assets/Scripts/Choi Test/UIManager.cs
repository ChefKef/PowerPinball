using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI roundTimerText;

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

    [SerializeField]
    private int roundLength;

    private float roundTimer;

    // Start is called before the first frame update
    void Start()
    {
        // Leverage width and height of scoreText's rectangle to ensure it fits
        // wholly onscreen.
        scoreText.rectTransform.position = new Vector3(
            scoreText.rectTransform.rect.width + leftMargin, 
            scoreText.rectTransform.rect.height + bottomMargin);

        // Do the same for the round timer.
        // Transforms of TMP's are at the centre of the textbox, not the top
        // left corner!
        roundTimerText.rectTransform.position = new Vector3(
            Screen.width / 2,
            Screen.height - roundTimerText.rectTransform.rect.height / 2);

        roundTimer = roundLength;
    }

    // Update is called once per frame
    void Update()
    {
        // Don't let the timer display negative values.
        if (roundTimer > 0) roundTimer -= Time.deltaTime;

        scoreText.text = "Score: " + GameManager.scoreP1;

        // Only show the remaining time as an integer.
        roundTimerText.text = ((int)roundTimer).ToString();
    }
}
