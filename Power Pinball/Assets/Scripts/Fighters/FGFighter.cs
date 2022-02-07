using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FGFighter
{

    Dictionary<string, FGAction> actions;

    FGFighter()
    {

    }

    public virtual void FGUpdate()
    {
        
    }

    //This is a separate call/structure for netcode reasons.
    //Not that we're necessarily doing netcode, but it's better to be proactive
    public virtual void FGDraw()
    {

    }

}
