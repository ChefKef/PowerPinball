using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FGAction
{

    public readonly FGHurtbox[,] hurtboxes; //both 2D arrays. outer layer = frame. inner layer = multiple hitboxes in one frame
    public readonly FGHitbox[,] hitboxes;   //to avoid storing a lot of data, if any given frame has 0 data we go to the most recent frame with data.
    private FGHurtbox[] lastHurt;           //These two variables are references to that "most recent frame", to avoid looping through the array every frame.
    private FGHitbox[] lastHit;
    private int duration;                   //in frames. measures the length of the move, not drawn animation frames
    private bool looping;                   //Whether or not this action "ends" at lastFrame
    private int loopFrame = 0;              //Which frame to return to at the end of a looping animation. default 0.
    //Sprites[] sprites //im not doing this right now
    
    public readonly int frame;


    public void FGAUpdate(FGFighter parent)
    {

    }

    public void FGADraw(FGRenderer renderer)
    {

    }


}
