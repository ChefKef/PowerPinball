using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FGScript {

public class FGAction
{

        //I want these to be readonly, but I'm waiting until content complete to make a constructor
        public FGHurtbox[][] hurtboxes;          //both 2D arrays. outer layer = frame. inner layer = multiple hitboxes in one frame
        public FGHitbox[][] hitboxes;            //to avoid storing a lot of data, if any given frame has 0 data we go to the most recent frame with data.
        private FGHurtbox[] lastHurt;           //These two variables are references to that "most recent frame" with data, to avoid looping through the array every frame.
        private FGHitbox[] lastHit;
        public int duration;                    //in frames. measures the length of the move, not drawn animation frames (so you could animate at less than 60fps)
        public bool looping;                    //Whether or not this action "ends" at lastFrame
        public int loopFrame = 0;               //Which frame to return to at the end of a looping animation. default 0.
        //Sprites[] sprites //im not doing this right now
        
        public int frame;
        public bool ended = false;
        public FGHurtbox[] CurrentHurt { get => lastHurt; }
        public FGHitbox[] CurrentHit { get => lastHit;  }

        /// <summary>
        /// Makes a new default action.
        /// </summary>
        /// <param name="duration">The length of the animation from start to finish</param>
        /// <param name="looping">Whether or not to repeat the animation after it finishes</param>
        /// <param name="loopFrame">Which frame to jump to after finishing, if looping is enabled.</param>
        public FGAction(int duration, bool looping, int loopFrame = 0)
        {
            hurtboxes = new FGHurtbox[duration][];
            hitboxes = new FGHitbox[duration][];

            this.duration = duration;
            this.looping = looping;
            this.loopFrame = loopFrame;

        }

        public virtual void FGAUpdate(FGFighter parent)
        {
            frame++;

            if(frame >= duration && looping)
                frame = loopFrame;
            else if (frame >= duration - 1 && !looping) //We need to flag "ended" in advance, because we can't/won't react to it until next frame
                ended = true;


            //Find the most recent hurtbox with any data
            if (hurtboxes[frame] != null) lastHurt = hurtboxes[frame];
            if(hitboxes != null)
                if (hitboxes[frame] != null) lastHit = hitboxes[frame];         //To signal the end of a hitbox, simply have an empty array at the given frame.
            


        }

        public virtual void FGADraw(FGRenderer renderer)
        {

        }

        public virtual void FGADrawHitboxes(FGRenderer renderer)
        {
            renderer.DrawCollision(this);
        }

        public void SetActive()
        {
            frame = -1; //Update will 100% get called sometime after this function call, but before Draw(). If it doesn't, we messed up somewhere.
            ended = false;
            lastHurt = hurtboxes[0];
            lastHit = hitboxes[0];
        }
        
        //Creates a default action - a 1 frame looping animation of a hurtbox, ideal for Idle poses
        public static FGAction newDefaultAction()
        {
            FGAction val = new FGAction(1, true);

            val.hurtboxes[0] = new FGHurtbox[1];
            val.hurtboxes[0][0] = new FGHurtbox(new Rect(-0.5f, 2, 1, 2));

            return val;
        }

}

}