using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourProfile : MonoBehaviour
{
    public Dictionary<Customisables, Color> profile;

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
