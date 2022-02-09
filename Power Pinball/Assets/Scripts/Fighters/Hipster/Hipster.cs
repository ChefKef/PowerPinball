using FGScript;

public class Hipster : FGFighter
{

    //In this class I'm going to initialize all the hitbox/hurtbox data
    //This is cringe I'm just too lazy to do File IO in the next ~3 days

    public Hipster(FGRenderer renderer) : base (renderer)
    {

        maxGroundSpeed = 0.2f;
        maxAirSpeed = 0.18f;
        groundAcceleration = 0.04f;
        friction = 0.75f;
        airAcceleration = 0.005f;
        jumpVelocity = 0.55f;
        gravity = 0.04f;


        actions["crouch"].hurtboxes = new FGHurtbox[4][];
        actions["crouch"].hitboxes = new FGHitbox[4][];
        actions["crouch"].hurtboxes[0] = new FGHurtbox[1];
        actions["crouch"].hurtboxes[1] = new FGHurtbox[1];
        actions["crouch"].hurtboxes[2] = new FGHurtbox[1];
        actions["crouch"].hurtboxes[3] = new FGHurtbox[1];
        actions["crouch"].hurtboxes[0][0] = new FGHurtbox();
        actions["crouch"].hurtboxes[1][0] = new FGHurtbox();
        actions["crouch"].hurtboxes[2][0] = new FGHurtbox();
        actions["crouch"].hurtboxes[3][0] = new FGHurtbox();
        actions["crouch"].hurtboxes[0][0].rect = new UnityEngine.Rect(-0.5f, 1.8f, 1, 1.8f);
        actions["crouch"].hurtboxes[1][0].rect = new UnityEngine.Rect(-0.5f, 1.6f, 1, 1.6f);
        actions["crouch"].hurtboxes[2][0].rect = new UnityEngine.Rect(-0.5f, 1.3f, 1, 1.3f);
        actions["crouch"].hurtboxes[3][0].rect = new UnityEngine.Rect(-0.5f, 1f, 1, 1f);
        actions["crouch"].duration = 4;
        actions["crouch"].loopFrame = 3;
        actions["crouch"].looping = true;

    }

}
