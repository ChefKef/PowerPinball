using FGScript;
using UnityEngine;

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

        actions["air"].sprites[0] = new Sprite[5];
        actions["air"].sprites[0][0] = Resources.Load<Sprite>("CustomCharacter/Jump/Skin") as Sprite;
        actions["air"].sprites[0][1] = Resources.Load<Sprite>("CustomCharacter/Jump/Pants") as Sprite;
        actions["air"].sprites[0][2] = Resources.Load<Sprite>("CustomCharacter/Jump/Shoes") as Sprite;
        actions["air"].sprites[0][3] = Resources.Load<Sprite>("CustomCharacter/Jump/Shirt") as Sprite;
        actions["air"].sprites[0][4] = Resources.Load<Sprite>("CustomCharacter/Jump/Hair") as Sprite;

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
        actions["poke"].hitboxes[3][0] = new FGHitbox(new UnityEngine.Rect(0, 1.8f, 1.3f, 1), new UnityEngine.Vector2(2f, 4f));
        actions["poke"].hitboxes[6] = new FGHitbox[0];
        actions["poke"].sprites[0] = actions["idle"].sprites[0];
        actions["poke"].sprites[3] = new Sprite[5];
        actions["poke"].sprites[3][0] = Resources.Load<Sprite>("CustomCharacter/Punch/Skin") as Sprite;
        actions["poke"].sprites[3][1] = Resources.Load<Sprite>("CustomCharacter/Punch/Pants") as Sprite;
        actions["poke"].sprites[3][2] = Resources.Load<Sprite>("CustomCharacter/Punch/Shoes") as Sprite;
        actions["poke"].sprites[3][3] = Resources.Load<Sprite>("CustomCharacter/Punch/Shirt") as Sprite;
        actions["poke"].sprites[3][4] = Resources.Load<Sprite>("CustomCharacter/Punch/Hair") as Sprite;
        actions["poke"].sprites[6] = actions["idle"].sprites[0];

        actions["spike"] = new HipsterSpike();

        actions["launch"] = new HipsterLaunch();
        actions["launch"].sprites[0] = actions["air"].sprites[0];

        actions["airPoke"] = actions["poke"];

        actions["airSpike"] = new FGAction(18, false);
        actions["airSpike"].hurtboxes[0] = new FGHurtbox[1];
        actions["airSpike"].hurtboxes[0][0] = new FGHurtbox(new UnityEngine.Rect(-0.4f, 2.45f, 1, 2.45f));
        actions["airSpike"].hitboxes[3] = new FGHitbox[1];
        actions["airSpike"].hitboxes[3][0] = new FGHitbox(new UnityEngine.Rect(0, 0.8f, 1.5f, 1.8f), new UnityEngine.Vector2(50, -50));
        actions["airSpike"].hitboxes[6] = new FGHitbox[0];
        actions["airPoke"].sprites[0] = actions["air"].sprites[0];
        actions["airPoke"].sprites[3] = actions["poke"].sprites[3];
        actions["airPoke"].sprites[6] = actions["air"].sprites[0];

        actions["airLaunch"] = actions["launch"];

        actions["autoCombo2"] = new FGAction(25, false);
        actions["autoCombo2"].hurtboxes[0] = new FGHurtbox[1];
        actions["autoCombo2"].hurtboxes[0][0] = new FGHurtbox(new UnityEngine.Rect(-0.4f, 2.45f, 1, 2.45f));
        actions["autoCombo2"].hitboxes[5] = new FGHitbox[1];
        actions["autoCombo2"].hitboxes[5][0] = new FGHitbox(new UnityEngine.Rect(-0.2f, 1.5f, 1.7f, 1.5f), new UnityEngine.Vector2(20f, 30) * 1.5f);
        actions["autoCombo2"].hitboxes[8] = new FGHitbox[0];


        actions["autoCombo3"] = actions["launch"];


    }

}
