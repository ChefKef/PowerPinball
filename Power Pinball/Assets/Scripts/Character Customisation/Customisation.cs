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
    // Text describing the part currently being customised.
    [SerializeField] private TextMeshProUGUI currentCustomisableTextP1;
    [SerializeField] private TextMeshProUGUI currentCustomisableTextP2;

    /// <summary>
    /// Text describing the current selected round length.
    /// </summary>
    [SerializeField] private TextMeshProUGUI currentRoundLengthText;

    // Sprites for the customisable parts.
    [SerializeField] private Image[] spritesP1;
    [SerializeField] private Image[] spritesP2;

    // Third-party colour pickers.
    [SerializeField] private ColorPickerP1 colourPickerP1;
    [SerializeField] private ColorPickerP2 colourPickerP2;

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
    
    // The current part being customsied.
    private Customisables currentCustomisableP1;
    private Customisables currentCustomisableP2;

    /// <summary>
    /// Cycle next in the customisables array.
    /// </summary>
    public void NextCustomisable(int player)
    {
        // Load the colour and assign it to the sliders.
        switch (player)
        {
            case 1:
                // Circular array implementation.
                currentCustomisableP1 = (Customisables)(
                    ((int)currentCustomisableP1 + 1) 
                    % customisables.Length);

                Color newColourP1 = ColourProfileManager.p1ColourProfile.profile[currentCustomisableP1];
                colourPickerP1.color = newColourP1;

                // Update text label.
                currentCustomisableTextP1.text = customisables[(int)currentCustomisableP1].ToString();
                
                break;
            case 2:
                // Circular array implementation.
                currentCustomisableP2 = (Customisables)(
                    ((int)currentCustomisableP2 + 1)
                    % customisables.Length);

                Color newColourP2 = ColourProfileManager.p2ColourProfile.profile[currentCustomisableP2];
                colourPickerP2.color = newColourP2;

                // Update text label.
                currentCustomisableTextP2.text = customisables[(int)currentCustomisableP2].ToString();

                break;
        }
    }

    /// <summary>
    /// Cycle previous in the customisables array.
    /// </summary>
    public void PreviousCustomisable(int player)
    {
        // Load the colour and assign it to the sliders.
        switch (player)
        {
            case 1:
                // Circular array implementation.
                // Account for the fact that -1 % y = -1 (ugh).
                if (--currentCustomisableP1 < 0) 
                    currentCustomisableP1 = (Customisables)(customisables.Length - 1);

                Color newColourP1 = ColourProfileManager.p1ColourProfile.profile[currentCustomisableP1];
                colourPickerP1.color = newColourP1;

                // Update text label.
                currentCustomisableTextP1.text = customisables[(int)currentCustomisableP1].ToString();

                break;
            case 2:
                // Circular array implementation.
                // Account for the fact that -1 % y = -1 (ugh).
                if (--currentCustomisableP2 < 0)
                    currentCustomisableP2 = (Customisables)(customisables.Length - 1);

                Color newColourP2 = ColourProfileManager.p2ColourProfile.profile[currentCustomisableP2];
                colourPickerP2.color = newColourP2;

                // Update text label.
                currentCustomisableTextP2.text = customisables[(int)currentCustomisableP2].ToString();

                break;
        }
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
    /// Commit colour choices to the ColourProfile for P1.
    /// </summary>
    private void SaveChangesP1(Color colour)
    {
        ColourProfileManager.p1ColourProfile.profile[currentCustomisableP1] = colour;
    }

    /// <summary>
    /// Commit colour choices to the ColourProfile for P2.
    /// </summary>
    private void SaveChangesP2(Color colour)
    {
        ColourProfileManager.p2ColourProfile.profile[currentCustomisableP2] = colour;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentCustomisableP1 = 0;
        currentCustomisableP2 = 0;

        // Commit changes when colour picker values are changed.
        colourPickerP1.onColorChanged += SaveChangesP1;
        colourPickerP2.onColorChanged += SaveChangesP2;

        // Load ColourProfile and display colours on the sprite.
        for (int i = 0; i < spritesP1.Length; i++)
            spritesP1[i].color = ColourProfileManager.p1ColourProfile.profile[customisables[i]];
        
        for (int i = 0; i < spritesP2.Length; i++)
            spritesP2[i].color = ColourProfileManager.p2ColourProfile.profile[customisables[i]];

        //// Initialise the slider colours to match the first item in the array.
        //rSlider.value = sprites[0].color.r;
        //gSlider.value = sprites[0].color.g;
        //bSlider.value = sprites[0].color.b;

        // Initialise the colour picker to match the first array element's colour.
        colourPickerP1.color = spritesP1[0].color;
        colourPickerP2.color = spritesP2[0].color;

        // Initialise text labels.
        currentCustomisableTextP1.text = customisables[(int)currentCustomisableP1].ToString();
        currentCustomisableTextP2.text = customisables[(int)currentCustomisableP2].ToString();
        currentRoundLengthText.text = ((int)roundLength).ToString() + "s";
    }

    // Update is called once per frame
    void Update()
    {
        // Update sprite colours each frame.
        spritesP1[(int)currentCustomisableP1].color = colourPickerP1.color;
        spritesP2[(int)currentCustomisableP2].color = colourPickerP2.color;
    }
}
