using System.Collections;
using System.Collections.Generic;
using FGScript;

public class HipsterLaunch : FGAction
{
    public HipsterLaunch(int duration = 30, bool looping = false, int loopFrame = 0) : base(duration, looping, loopFrame)
    {

        hurtboxes[0] = new FGHurtbox[1];
        hurtboxes[0][0] = new FGHurtbox(new UnityEngine.Rect(-0.4f, 1.6f, 1, 1.6f));
        hurtboxes[2] = new FGHurtbox[1];
        hurtboxes[2][0] = new FGHurtbox(new UnityEngine.Rect(-0.4f, 1.7f, 1, 1.7f));
        hurtboxes[3] = new FGHurtbox[1];
        hurtboxes[3][0] = new FGHurtbox(new UnityEngine.Rect(-0.4f, 1.8f, 1, 1.8f));
        hurtboxes[4] = new FGHurtbox[1];
        hurtboxes[4][0] = new FGHurtbox(new UnityEngine.Rect(-0.4f, 2.0f, 1, 2.0f));


        hitboxes[2] = new FGHitbox[1];
        hitboxes[2][0] = new FGHitbox(new UnityEngine.Rect(-0.3f, 2.2f, 1.6f, 1.6f), new UnityEngine.Vector2(15f, 90) * 3.3f);
        hitboxes[4] = new FGHitbox[1];
        hitboxes[4][0] = new FGHitbox(new UnityEngine.Rect(0.2f, 2.4f, 0.6f, 1.2f), new UnityEngine.Vector2(10f, 50) * 3.3f);
        hitboxes[14] = new FGHitbox[0];

    }


    public override void FGAUpdate(FGFighter parent)
    {
        base.FGAUpdate(parent);


        if(frame == 4)
        {
            parent.state = FGFighterState.airAttack;
            parent.velocity = new UnityEngine.Vector2(0.05f * (parent.facingLeft ? -1 : 1), 0.55f);
        }

    }


}
