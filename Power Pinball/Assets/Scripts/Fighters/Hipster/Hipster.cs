using FGScript;

public class Hipster : FGFighter
{

    //In this class I'm going to initialize all the hitbox/hurtbox data
    //This is cringe I'm just too lazy to do File IO in the next ~3 days

    public Hipster(FGRenderer renderer) : base (renderer)
    {

        maxGroundSpeed = 0.2f;
        maxAirSpeed = 0.18f;

        actions["crouch"].hurtboxes = new FGHurtbox[3][];
        actions["crouch"].hurtboxes[0] = new FGHurtbox[1];
        actions["crouch"].hurtboxes[1] = new FGHurtbox[1];
        actions["crouch"].hurtboxes[2] = new FGHurtbox[1];
        actions["crouch"].hurtboxes[0][0] = new FGHurtbox();
        actions["crouch"].hurtboxes[1][0] = new FGHurtbox();
        actions["crouch"].hurtboxes[2][0] = new FGHurtbox();
        actions["crouch"].hurtboxes[0][0].rect = new UnityEngine.Rect(-0.5f, 1.7f, 1, 1.7f);
        actions["crouch"].hurtboxes[1][0].rect = new UnityEngine.Rect(-0.5f, 1.4f, 1, 1.4f);
        actions["crouch"].hurtboxes[2][0].rect = new UnityEngine.Rect(-0.5f, 1f, 1, 1f);
        actions["crouch"].duration = 3;
        actions["crouch"].loopFrame = 2;

    }

}
