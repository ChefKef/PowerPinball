using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Customisables
{
    Hair,
    Shirt,
    Pants,
    Shoes
}

public class CharacterCustomisation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentCustomisableText;
    [SerializeField] private Slider rSlider;
    [SerializeField] private Slider gSlider;
    [SerializeField] private Slider bSlider;
    [SerializeField] private Image[] sprites;

    private Customisables[] customisables;
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
        customisables = new Customisables[] 
        { 
            Customisables.Hair, 
            Customisables.Shirt, 
            Customisables.Pants, 
            Customisables.Shoes
        };
        currentCustomisable = 0;

        rSlider.onValueChanged.AddListener(delegate { SaveChanges(); });
        gSlider.onValueChanged.AddListener(delegate { SaveChanges(); });
        bSlider.onValueChanged.AddListener(delegate { SaveChanges(); });

        Debug.Log(ColourProfileManager.p1ColourProfile);

        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].color = ColourProfileManager.p1ColourProfile.profile[customisables[i]];
        }

        rSlider.value = sprites[0].color.r;
        gSlider.value = sprites[0].color.g;
        bSlider.value = sprites[0].color.b;
    }

    // Update is called once per frame
    void Update()
    {
        currentCustomisableText.text = customisables[(int)currentCustomisable].ToString();
        sprites[(int)currentCustomisable].color = new Color(
            rSlider.value,
            gSlider.value,
            bSlider.value);
    }
}
