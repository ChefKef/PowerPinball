using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FGFighterState
{
    intro, //intro animation??? skipping for now
    idle,
    run, 
    crouch,
    air,
    attack,
    airAttack,
    jump, //jumpsquat, if any
    land, //landing lag
    dead
}

namespace FGScript {

public class FGFighter
{
    //consts
    protected const float groundLocationY = 0; //TODO don't fucking do this

    //Serialized Data
    protected Dictionary<string, FGAction> actions;
    protected float maxGroundSpeed;
    protected float maxAirSpeed; //horizontal
    protected float airAcceleration;

    //State data
    public Vector2 position, velocity; //coordinate system: x horizontal y vertical, positive is up & right
    public FGFighterState state = FGFighterState.idle; //this is useful as a separate concept from currentAction, so we can do things like handle air drift the same regardless of if attacking or not.
    protected FGAction currentAction;
    protected int cFrame; //current frame; how far along we are in a given action.
    protected int hitstunFrames; //if > 0, we skip updating that frame for visual/gamefeel reasons
    protected bool hit; //generally: whether the attack we used connected. Set to true when it's possible to cancel into an action
    protected bool facingLeft;

    //Passthroughs
    public FGHurtbox[,] Hurtboxes { get => currentAction.hurtboxes; }
    public FGHitbox[,] Hitboxes { get => currentAction.hitboxes; }

    //Unity crap
    FGRenderer renderer;

    //Buttons and input
    protected Vector2 joystick; //Digital, values should be -1 0 or 1.
    protected Vector2 oldJoystick;
    protected bool poke;
    protected bool oldPoke;
    protected bool spike;
    protected bool oldSpike;
    protected bool launch;
    protected bool oldLaunch;

    //Button callbacks

    public FGFighter(FGRenderer renderer)
    {
        actions = new Dictionary<string, FGAction>();
        this.renderer = renderer;
    }

    public virtual void FGUpdate()
    {
        currentAction.FGAUpdate(this);
    }

    //This is a separate call/structure for netcode reasons.
    //Not that we're necessarily doing netcode, but it's better to be proactive
    public virtual void FGDraw()
    {
        currentAction.FGADraw(renderer);
    }

}

}