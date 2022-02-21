using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private TextMeshProUGUI roundTimerText;

    [SerializeField] private TextMeshProUGUI countdownTimerText;

    /// <summary>
    /// How far to push the TMP rectangle right.
    /// </summary>
    [SerializeField] private int leftMargin;

    /// <summary>
    /// How far to push the TMP rectangle up.
    /// </summary>
    [SerializeField] private int bottomMargin;

    /// <summary>
    /// Duration of the round, in seconds.
    /// </summary>
    [SerializeField] private int roundLength;

    /// <summary>
    /// Tracks the time remaining in the round.
    /// </summary>
    private float roundTimer;

    // TODO: Change back to 3; load time between pressing play and the game actually starting is counted.
    // What a joke.
    private const int CountdownLength = 6;
    private float countdownTimer;

    /// <summary>
    /// Flag to determine whether to set timeScale to 0 or 1.
    /// </summary>
    public bool IsCountingDown { get; private set; }

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
        countdownTimer = CountdownLength;

        // Initialise text to the appropriate values before game start.
        scoreText.text = "Score: 0";
        roundTimerText.text = ((int)roundTimer).ToString();

        IsCountingDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Show the countdown panel.
        if (IsCountingDown)
        {
            if (countdownTimer > 0)
            {
                // timeScale is set to 0 in GameManager, so we need to use
                // timeScale-independent intervals.
                countdownTimer -= Time.unscaledDeltaTime;
                countdownTimerText.text = ((int)countdownTimer + 1).ToString();
            }
            // Hide the countdown panel and begin play.
            else IsCountingDown = false;
        }
        // Update UI text values.
        else
        {
            // Don't let the timer display negative values.
            if (roundTimer > 0) roundTimer -= Time.deltaTime;

            scoreText.text = "Score: " + GameManager.scoreP1;

            // Only show the remaining time as an integer.
            roundTimerText.text = ((int)roundTimer).ToString();
        }
    }
}
