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
        friction = 0.55f;
        airAcceleration = 0.0055f;
        jumpVelocity = 0.55f;
        gravity = 0.04f;

        actions["crouch"] = new FGAction(4, true, 3);
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


        actions["poke"] = new FGAction(18, false);
        actions["poke"].hurtboxes[0] = new FGHurtbox[1];
        actions["poke"].hurtboxes[0][0] = new FGHurtbox();
        actions["poke"].hurtboxes[0][0].rect = new UnityEngine.Rect(-0.5f, 2f, 1, 2);
        actions["poke"].hitboxes[3] = new FGHitbox[1];
        actions["poke"].hitboxes[3][0] = new FGHitbox();
        actions["poke"].hitboxes[3][0].rect = new UnityEngine.Rect(0, 1.8f, 1.5f, 1);
        actions["poke"].hitboxes[6] = new FGHitbox[0];

        actions["airPoke"] = actions["poke"];


    }

}
