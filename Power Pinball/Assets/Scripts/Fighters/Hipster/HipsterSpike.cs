using System.Collections;
using System.Collections.Generic;
using FGScript;
using UnityEngine;

public class HipsterSpike : FGAction
{

    public HipsterSpike(int duration = 44, bool looping = false, int loopFrame = 0) : base(duration, looping, loopFrame)
    {

        hurtboxes[0] = new FGHurtbox[1];
        hurtboxes[0][0] = new FGHurtbox(new UnityEngine.Rect(-0.4f, 2f, 1, 2));
        hurtboxes[1] = new FGHurtbox[1];
        hurtboxes[1][0] = new FGHurtbox(new UnityEngine.Rect(-0.4f, 1.8f, 1, 1.8f));
        hurtboxes[2] = new FGHurtbox[1];
        hurtboxes[2][0] = new FGHurtbox(new UnityEngine.Rect(-0.4f, 1.6f, 1, 1.6f));
        hurtboxes[3] = new FGHurtbox[1];
        hurtboxes[3][0] = new FGHurtbox(new UnityEngine.Rect(-0.4f, 1.4f, 1, 1.4f));
        hurtboxes[4] = new FGHurtbox[1];
        hurtboxes[4][0] = new FGHurtbox(new UnityEngine.Rect(-0.4f, 1.2f, 1, 1.2f));
        hurtboxes[5] = new FGHurtbox[1];
        hurtboxes[5][0] = new FGHurtbox(new UnityEngine.Rect(-0.4f, 1.0f, 1, 1.0f));

        hitboxes[6] = new FGHitbox[2];
        hitboxes[6][0] = new FGHitbox(new UnityEngine.Rect(0, 0.5f, 2f, 0.5f), new UnityEngine.Vector2(20f, 30) * 1.5f);
        hitboxes[6][1] = new FGHitbox(new UnityEngine.Rect(0, 1.1f, 0.85f, 0.6f), new UnityEngine.Vector2(20f, 30) * 1.5f);
        hitboxes[10] = new FGHitbox[1];
        hitboxes[10][0] = hitboxes[6][0];
        hitboxes[25] = new FGHitbox[0];

        hurtboxes[43] = hurtboxes[0];
        hurtboxes[42] = hurtboxes[1];
        hurtboxes[41] = hurtboxes[2];
        hurtboxes[40] = hurtboxes[3];
        hurtboxes[39] = hurtboxes[4];
        hurtboxes[38] = hurtboxes[5];

        sprites[0] = new Sprite[5];
        sprites[0][0] = Resources.Load<Sprite>("CustomCharacter/Slide_skin") as Sprite;
        sprites[0][1] = Resources.Load<Sprite>("CustomCharacter/Slide_pants") as Sprite;
        sprites[0][2] = Resources.Load<Sprite>("CustomCharacter/Slide_shoes") as Sprite;
        sprites[0][3] = Resources.Load<Sprite>("CustomCharacter/Slide_shirt") as Sprite;
        sprites[0][4] = Resources.Load<Sprite>("CustomCharacter/Slide_hair") as Sprite;
        spriteOffset = new Vector2(6, 0);


    }

    public override void FGAUpdate(FGFighter parent)
    {
        base.FGAUpdate(parent);


        if (frame >= 6 && frame < 25)
        {
            parent.velocity = new UnityEngine.Vector2(0.3f * (parent.facingLeft ? -1 : 1), 0);
        }
        else if (frame >= 35)
        {
            parent.velocity = new UnityEngine.Vector2(0.08f * (parent.facingLeft ? -1 : 1), 0);
        }
        else if (frame >= 30)
        {
            parent.velocity = new UnityEngine.Vector2(0.13f * (parent.facingLeft ? -1 : 1), 0);
        }
        else if(frame >= 25)
        {
            parent.velocity = new UnityEngine.Vector2(0.23f * (parent.facingLeft ? -1 : 1), 0);
        }
        

        parent.position = new UnityEngine.Vector2(parent.position.x + parent.velocity.x, parent.position.y);

    }

}
