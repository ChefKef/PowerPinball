using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Customisable parts of the player sprite.
/// </summary>
public enum Customisables
{
    Hair,
    Shirt,
    Pants,
    Shoes
}

public enum RoundLengths
{
    Sixty = 1/*60*/,
    Ninety = 2/*90*/
}

public class Customisation : MonoBehaviour
{
    /// <summary>
    /// Text describing the part currently being customised.
    /// </summary>
    [SerializeField] private TextMeshProUGUI currentCustomisableText;

    /// <summary>
    /// Text describing the current selected round length.
    /// </summary>
    [SerializeField] private TextMeshProUGUI currentRoundLengthText;

    /// <summary>
    /// Sprites for the customisable parts.
    /// </summary>
    [SerializeField] private Image[] sprites;

    /// <summary>
    /// Third-party colour picker.
    /// </summary>
    [SerializeField] private ColorPicker colourPicker;

    public static RoundLengths roundLength = RoundLengths.Sixty;

    ///// <summary>
    ///// Label displaying the numeric value of the R slider.
    ///// </summary>
    //[SerializeField] private TextMeshProUGUI rSliderValueLabel;

    ///// <summary>
    ///// Label displaying the numeric value of the R slider.
    ///// </summary>
    //[SerializeField] private TextMeshProUGUI gSliderValueLabel;

    ///// <summary>
    ///// Label displaying the numeric value of the R slider.
    ///// </summary>
    //[SerializeField] private TextMeshProUGUI bSliderValueLabel;

    ///// <summary>
    ///// Slider controlling R value.
    ///// </summary>
    //[SerializeField] private Slider rSlider;

    ///// <summary>
    ///// Slider controlling G value.
    ///// </summary>
    //[SerializeField] private Slider gSlider;

    ///// <summary>
    ///// Slider controlling B value.
    ///// </summary>
    //[SerializeField] private Slider bSlider;


    /// <summary>
    /// List of customisable parts. Used for UI display.
    /// </summary>
    private Customisables[] customisables = new Customisables[] 
    { 
        Customisables.Hair, 
        Customisables.Shirt, 
        Customisables.Pants, 
        Customisables.Shoes
    };
    
    /// <summary>
    /// The current part being customsied.
    /// </summary>
    private Customisables currentCustomisable;

    /// <summary>
    /// Cycle next in the customisables array.
    /// </summary>
    public void NextCustomisable()
    {
        // Circular array implementation.
        currentCustomisable = (Customisables)(
            ((int)currentCustomisable + 1) 
            % customisables.Length);

        // Load the colour and assign it to the sliders.
        Color newColour = ColourProfileManager.p1ColourProfile.profile[currentCustomisable];
        colourPicker.color = newColour;

        // Update text label.
        currentCustomisableText.text = customisables[(int)currentCustomisable].ToString();

        //rSlider.value = newColour.r;
        //gSlider.value = newColour.g;
        //bSlider.value = newColour.b;
    }

    /// <summary>
    /// Cycle previous in the customisables array.
    /// </summary>
    public void PreviousCustomisable()
    {
        // Circular array implementation.
        // Account for the fact that -1 % y = -1 (ugh).
        if (--currentCustomisable < 0) 
            currentCustomisable = (Customisables)(customisables.Length - 1);

        // Load the colour and assign it to the sliders.
        Color newColour = ColourProfileManager.p1ColourProfile.profile[currentCustomisable];
        colourPicker.color = newColour;

        // Update text label.
        currentCustomisableText.text = customisables[(int)currentCustomisable].ToString();

        //rSlider.value = newColour.r;
        //gSlider.value = newColour.g;
        //bSlider.value = newColour.b;
    }

    public void ChangeRoundLength()
    {
        switch (roundLength)
        {
            case RoundLengths.Sixty:
                roundLength = RoundLengths.Ninety;
                break;
            case RoundLengths.Ninety:
                roundLength = RoundLengths.Sixty;
                break;
        }

        // Update text label.
        currentRoundLengthText.text = ((int)roundLength).ToString() + "s";
    }

    /// <summary>
    /// Commit colour choices to the ColourProfile.
    /// </summary>
    private void SaveChanges(Color colour)
    {
        ColourProfileManager.p1ColourProfile.profile[currentCustomisable] = colour;
            //rSlider.value,
            //gSlider.value,
            //bSlider.value);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentCustomisable = 0;

        //// Commit changes when slider values are changed.
        //rSlider.onValueChanged.AddListener(delegate { SaveChanges(); });
        //gSlider.onValueChanged.AddListener(delegate { SaveChanges(); });
        //bSlider.onValueChanged.AddListener(delegate { SaveChanges(); });

        // Commit changes when colour picker values are changed.
        colourPicker.onColorChanged += SaveChanges;

        // Load ColourProfile and display colours on the sprite.
        for (int i = 0; i < sprites.Length; i++)
            sprites[i].color = ColourProfileManager.p1ColourProfile.profile[customisables[i]];

        //// Initialise the slider colours to match the first item in the array.
        //rSlider.value = sprites[0].color.r;
        //gSlider.value = sprites[0].color.g;
        //bSlider.value = sprites[0].color.b;

        // Initialise the colour picker to match the first array element's colour.
        colourPicker.color = sprites[0].color;

        // Initialise text labels.
        currentCustomisableText.text = customisables[(int)currentCustomisable].ToString();
        currentRoundLengthText.text = ((int)roundLength).ToString() + "s";
    }

    // Update is called once per frame
    void Update()
    {
        // Update sprite colours each frame.
        sprites[(int)currentCustomisable].color = colourPicker.color;/*new Color(
            rSlider.value,
            gSlider.value,
            bSlider.value);*/
        //rSliderValueLabel.text = ((int)(rSlider.value * 255)).ToString();
        //gSliderValueLabel.text = ((int)(gSlider.value * 255)).ToString();
        //bSliderValueLabel.text = ((int)(bSlider.value * 255)).ToString();
    }
}
