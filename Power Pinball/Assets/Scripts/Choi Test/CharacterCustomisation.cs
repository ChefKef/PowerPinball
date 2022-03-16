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

public class CharacterCustomisation : MonoBehaviour
{
    /// <summary>
    /// Text describing the part currently being customised.
    /// </summary>
    [SerializeField] private TextMeshProUGUI currentCustomisableText;

    /// <summary>
    /// Slider controlling R value.
    /// </summary>
    [SerializeField] private Slider rSlider;

    /// <summary>
    /// Slider controlling G value.
    /// </summary>
    [SerializeField] private Slider gSlider;

    /// <summary>
    /// Slider controlling B value.
    /// </summary>
    [SerializeField] private Slider bSlider;

    /// <summary>
    /// Sprites for the customisable parts.
    /// </summary>
    [SerializeField] private Image[] sprites;

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
        rSlider.value = newColour.r;
        gSlider.value = newColour.g;
        bSlider.value = newColour.b;
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
        rSlider.value = newColour.r;
        gSlider.value = newColour.g;
        bSlider.value = newColour.b;
    }

    /// <summary>
    /// Commit colour choices to the ColourProfile.
    /// </summary>
    private void SaveChanges()
    {
        ColourProfileManager.p1ColourProfile.profile[currentCustomisable] = new Color(
            rSlider.value,
            gSlider.value,
            bSlider.value);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentCustomisable = 0;

        // Commit changes when slider values are changed.
        rSlider.onValueChanged.AddListener(delegate { SaveChanges(); });
        gSlider.onValueChanged.AddListener(delegate { SaveChanges(); });
        bSlider.onValueChanged.AddListener(delegate { SaveChanges(); });

        // Load ColourProfile and display colours on the sprite.
        for (int i = 0; i < sprites.Length; i++)
            sprites[i].color = ColourProfileManager.p1ColourProfile.profile[customisables[i]];

        // Initialise the slider colours to match the first item in the array.
        rSlider.value = sprites[0].color.r;
        gSlider.value = sprites[0].color.g;
        bSlider.value = sprites[0].color.b;
    }

    // Update is called once per frame
    void Update()
    {
        // Update text and sprite colours each frame.
        currentCustomisableText.text = customisables[(int)currentCustomisable].ToString();
        sprites[(int)currentCustomisable].color = new Color(
            rSlider.value,
            gSlider.value,
            bSlider.value);
    }
}
