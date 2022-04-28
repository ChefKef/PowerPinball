using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourProfile
{
    /// <summary>
    /// Dictionary mapping enum of customisables to colours.
    /// </summary>
    public Dictionary<Customisables, Color> profile;

    /// <summary>
    /// Constructor initialising customisable colours to black.
    /// </summary>
    public ColourProfile()
    {
        profile = new Dictionary<Customisables, Color>()
        {
            { Customisables.Hair, Color.black },
            { Customisables.Shirt, Color.black },
            { Customisables.Pants, Color.black },
            { Customisables.Shoes, Color.black }
        };
    }
}
