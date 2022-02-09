using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FGScript {

public class FGAction
{

        //I want these to be readonly, but I'm waiting until content complete to make a constructor
        public FGHurtbox[][] hurtboxes;          //both 2D arrays. outer layer = frame. inner layer = multiple hitboxes in one frame
        public FGHitbox[][] hitboxes;            //to avoid storing a lot of data, if any given frame has 0 data we go to the most recent frame with data.
        public FGHurtbox[] lastHurt;           //These two variables are references to that "most recent frame", to avoid looping through the array every frame.
        public FGHitbox[] lastHit;
        public int duration;                    //in frames. measures the length of the move, not drawn animation frames
        public bool looping;                    //Whether or not this action "ends" at lastFrame
        public int loopFrame = 0;               //Which frame to return to at the end of a looping animation. default 0.
        //Sprites[] sprites //im not doing this right now
        
        public int frame;
        public FGHurtbox[] currentHurt;
        public FGHitbox[] currentHit;


        public void FGAUpdate(FGFighter parent)
        {

        }

        public void FGADraw(FGRenderer renderer)
        {

        }

        public void FGADrawHitboxes(FGRenderer renderer)
        {
            renderer.DrawCollision(this);
        }

        
        //Creates a default action - a 1 frame looping animation of a hurtbox, ideal for Idle poses
        public static FGAction newDefaultAction()
        {
            FGAction val = new FGAction();

            val.looping = true;
            val.duration = 1;
            val.hurtboxes = new FGHurtbox[1][];
            val.hurtboxes[0] = new FGHurtbox[1];
            val.hitboxes = new FGHitbox[1][];

            val.hurtboxes[0][0] = new FGHurtbox();
            val.hurtboxes[0][0].rect = new Rect(-0.5f, 2, 1, 2);


            //TEMP
            val.lastHurt = val.currentHurt = val.hurtboxes[0];

            return val;
        }

}

}