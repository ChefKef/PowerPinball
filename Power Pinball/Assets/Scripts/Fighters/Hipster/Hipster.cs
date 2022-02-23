using FGScript;

public class Hipster : FGFighter
{

    //In this class I'm going to initialize all the hitbox/hurtbox data
    //This is cringe I'm just too lazy to do File IO in the next ~3 days

    public Hipster(FGRenderer renderer) : base (renderer)
    {

        maxGroundSpeed = 0.23f;
        maxAirSpeed = 0.21f;
        groundAcceleration = 0.067f;
        friction = 0.65f;
        airAcceleration = 0.0055f;
        jumpVelocity = 0.55f;
        gravity = 0.04f;

        //(-0.4f, 2.45f, 1f, 2.45f)

        actions["crouch"] = new FGAction(4, true, 3);
        actions["crouch"].hurtboxes[0] = new FGHurtbox[1];
        actions["crouch"].hurtboxes[1] = new FGHurtbox[1];
        actions["crouch"].hurtboxes[2] = new FGHurtbox[1];
        actions["crouch"].hurtboxes[3] = new FGHurtbox[1];
        actions["crouch"].hurtboxes[0][0] = new FGHurtbox(new UnityEngine.Rect(-0.4f, 1.8f, 1, 1.8f));
        actions["crouch"].hurtboxes[1][0] = new FGHurtbox(new UnityEngine.Rect(-0.4f, 1.6f, 1, 1.6f));
        actions["crouch"].hurtboxes[2][0] = new FGHurtbox(new UnityEngine.Rect(-0.4f, 1.3f, 1, 1.3f));
        actions["crouch"].hurtboxes[3][0] = new FGHurtbox(new UnityEngine.Rect(-0.4f, 1f, 1, 1f));


        actions["poke"] = new FGAction(18, false);
        actions["poke"].hurtboxes[0] = new FGHurtbox[1];
        actions["poke"].hurtboxes[0][0] = new FGHurtbox(new UnityEngine.Rect(-0.4f, 2.45f, 1, 2.45f));
        actions["poke"].hitboxes[3] = new FGHitbox[1];
        actions["poke"].hitboxes[3][0] = new FGHitbox(new UnityEngine.Rect(0, 1.8f, 1.3f, 1), new UnityEngine.Vector2(0, 0));
        actions["poke"].hitboxes[6] = new FGHitbox[0];

        actions["spike"] = new HipsterSpike();

        actions["launch"] = new HipsterLaunch();

        actions["airPoke"] = actions["poke"];

        actions["airSpike"] = new FGAction(18, false);
        actions["airSpike"].hurtboxes[0] = new FGHurtbox[1];
        actions["airSpike"].hurtboxes[0][0] = new FGHurtbox(new UnityEngine.Rect(-0.4f, 2.45f, 1, 2.45f));
        actions["airSpike"].hitboxes[3] = new FGHitbox[1];
        actions["airSpike"].hitboxes[3][0] = new FGHitbox(new UnityEngine.Rect(0, 0.8f, 1.5f, 1.8f), new UnityEngine.Vector2(50, -50));
        actions["airSpike"].hitboxes[6] = new FGHitbox[0];

        actions["airLaunch"] = actions["launch"];


    }

}
