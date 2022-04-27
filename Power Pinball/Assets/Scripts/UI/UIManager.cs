using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI p1ScoreText;
    [SerializeField] private TextMeshProUGUI p1MultiplierText;
    [SerializeField] private TextMeshProUGUI p2ScoreText;
    [SerializeField] private TextMeshProUGUI p2MultiplierText;
    private RectTransform canvasRect;
    [SerializeField] private TextMeshProUGUI roundTimerText;
    [SerializeField] private TextMeshProUGUI countdownTimerText;
    [SerializeField] private TextMeshProUGUI overlayScoreText;
    [SerializeField] private GameObject countdown;
    [SerializeField] private GameObject overlay;
    [SerializeField] private GameObject howToPlayButton;

    /// <summary>
    /// How far to push the TMP rectangle right.
    /// </summary>
    [SerializeField] private int leftMargin;

    /// <summary>
    /// How far to push the TMP rectangle left.
    /// </summary>
    [SerializeField] private int rightMargin;

    /// <summary>
    /// How far to push the TMP rectangle up.
    /// </summary>
    [SerializeField] private int bottomMargin;

    ///// <summary>
    ///// Duration of the round, in seconds.
    ///// </summary>
    //[SerializeField] private int roundLength;

    /// <summary>
    /// Tracks the time remaining in the round.
    /// </summary>
    public float roundTimer;

    // TODO: Change back to 3; load time between pressing play and the game actually starting is counted.
    // What a joke.
    private const int CountdownLength = 6;
    private float countdownTimer;

    /// <summary>
    /// Flag to determine whether to set timeScale to 0 or 1 based on whether 
    /// the countdown has ended.
    /// </summary>
    public static bool isCountingDown;

    /// <summary>
    /// Flag to determine whether to set timeScale to 0 or 1 based on whether
    /// the round has ended.
    /// </summary>
    public static bool gameOver;

    /// <summary>
    /// Initialisation method.
    /// </summary>
    public void Init()
    {
        canvasRect = GetComponent<Canvas>().GetComponent<RectTransform>();

        // Leverage width and height of scoreText's rectangle to ensure it fits
        // wholly onscreen.
        p1ScoreText.rectTransform.position = new Vector3(
            p1ScoreText.rectTransform.rect.width + leftMargin,
            p1ScoreText.rectTransform.rect.height + bottomMargin);
        p2ScoreText.rectTransform.position = new Vector3(
            canvasRect.rect.width * 2/* + p2ScoreText.rectTransform.rect.width + leftMargin*/,
            p2ScoreText.rectTransform.rect.height + bottomMargin);

        // Multiplier text goes above the score.
        p1MultiplierText.rectTransform.position = new Vector3(
            p1ScoreText.rectTransform.rect.width + leftMargin,
            p1ScoreText.rectTransform.rect.height * 2 
                + bottomMargin
                + p1MultiplierText.rectTransform.rect.height);
        p2MultiplierText.rectTransform.position = new Vector3(
            canvasRect.rect.width * 2 /*+ p2ScoreText.rectTransform.rect.width + leftMargin*/,
            p2ScoreText.rectTransform.rect.height * 2
                + bottomMargin
                + p2MultiplierText.rectTransform.rect.height);

        // Do the same for the round timer.
        // Transforms of TMP's are at the centre of the textbox, not the top
        // left corner!
        roundTimerText.rectTransform.position = new Vector3(
            Screen.width / 2,
            Screen.height - roundTimerText.rectTransform.rect.height / 2);

        //roundTimer = (float)Customisation.roundLength;
        countdownTimer = CountdownLength;

        // Initialise text to the appropriate values before game start.
        p1ScoreText.text = "Score: 0";
        p2ScoreText.text = "Score: 0";
        roundTimerText.text = ((int)roundTimer).ToString();

        isCountingDown = true;
        gameOver = false;

        overlay.SetActive(false);
        howToPlayButton.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        // Show the countdown panel.
        if (isCountingDown)
        {
            // Hide the countdown.
            countdown.SetActive(true);

            if (countdownTimer > 0)
            {
                // timeScale is set to 0 in GameManager, so we need to use
                // timeScale-independent intervals.
                countdownTimer -= Time.unscaledDeltaTime;
                countdownTimerText.text = ((int)countdownTimer + 1).ToString();
            }
            // Hide the countdown panel and begin play.
            else isCountingDown = false;
        }
        // Update UI text values.
        else
        {
            // Hide the countdown.
            countdown.SetActive(false);

            // Show game UI.
            if (!gameOver)
            {
                // Don't let the timer display negative values.
                if (roundTimer > 0) roundTimer -= Time.deltaTime;
                // TODO: add case where game ends once score target is reached
                else gameOver = true;

                p1ScoreText.text = "Score: " + GameManager.scoreP1;
                p2ScoreText.text = "Score: " + GameManager.scoreP2;

                // Only show the remaining time as an integer.
                roundTimerText.text = ((int)roundTimer).ToString();
            }
            // Show game over overlay.
            else
            {
                overlayScoreText.text = "Final Score: " + GameManager.scoreP1;
                overlay.SetActive(true);
                howToPlayButton.SetActive(true);
            }
        }
    }
}