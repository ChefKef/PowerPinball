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
        protected float groundAcceleration;
        protected float friction;
        protected float airAcceleration;
    
        //State data
        public Vector2 position, velocity; //coordinate system: x horizontal y vertical, positive is up & right
        public FGFighterState state = FGFighterState.idle; //this is useful as a separate concept from currentAction, so we can do things like handle air drift the same regardless of if attacking or not.
        private FGAction currentAction;
        protected int cFrame; //current frame; how far along we are in a given action.
        protected int hitstunFrames; //if > 0, we skip updating that frame for visual/gamefeel reasons
        protected bool hit; //generally: whether the attack we used connected. Set to true when it's possible to cancel into an action
        public bool facingLeft;
    
        //Passthroughs
        public FGAction CurrentAction {
            get => currentAction;
            set
            {
                currentAction = value;
                currentAction.SetActive();
            }
        
        }
        public FGHurtbox[][] Hurtboxes { get => CurrentAction.hurtboxes; }
        public FGHitbox[][] Hitboxes { get => CurrentAction.hitboxes; }
    
        //Unity crap
        protected FGRenderer renderer;
    
        //Buttons and input
        protected Vector2 joystick; //Digital, values should be -1 0 or 1.
        protected Vector2 oldJoystick;
        protected bool poke;
        protected bool oldPoke;
        protected bool spike;
        protected bool oldSpike;
        protected bool launch;
        protected bool oldLaunch;
        protected bool jump;
        protected bool oldJump;

        //Button callbacks
#region Input
        public void MoveStick(Vector2 stick)
        {
            if (stick.x == 0) joystick.x = 0;
            else if (stick.x > 0) joystick.x = 1;
            else joystick.x = -1;

            if (stick.y == 0) joystick.y = 0;
            else if (stick.y > 0) joystick.x = 1;
            else joystick.y = -1;
        }
        public void PokeBtn(bool btn)
        {
            poke = btn;
        }
        public void SpikeBtn(bool btn)
        {
            spike = btn;
        }
        public void LaunchBtn(bool btn)
        {
            launch = btn;
        }
        public void JumpBtn(bool btn)
        {
            jump = btn;
        }
#endregion

        public FGFighter(FGRenderer renderer)
        {
            actions = new Dictionary<string, FGAction>();
            this.renderer = renderer;

            //Saves some time in testing.
            actions["idle"] = FGAction.newDefaultAction();
            actions["run"] = FGAction.newDefaultAction();
            actions["crouch"] = FGAction.newDefaultAction();
            actions["air"] = FGAction.newDefaultAction();

            CurrentAction = actions["idle"];
            state = FGFighterState.idle;

        }
    
        public virtual void FGUpdate()
        {
            if(currentAction.ended)
            {
                throw new System.NotImplementedException();
            }

            
            switch (state)
            {
                default:
                case FGFighterState.idle:
                case FGFighterState.crouch:
                    position = new Vector2(position.x + velocity.x, 0);
                    velocity *= friction;

                    if (joystick.x == 1 && joystick.y != -1)
                    {
                        state = FGFighterState.run;
                        CurrentAction = actions["run"];
                        facingLeft = false;
                    }
                    else if (joystick.x == -1 && joystick.y != -1)
                    {
                        state = FGFighterState.run;
                        CurrentAction = actions["run"];
                        facingLeft = true;
                    }
                    else if(joystick.y == -1 && state != FGFighterState.crouch)
                    {
                        state = FGFighterState.crouch;
                        CurrentAction = actions["crouch"];
                    }
                    else if(joystick.y != -1 && state == FGFighterState.crouch)
                    {
                        state = FGFighterState.idle;
                        CurrentAction = actions["idle"];
                    }


                    break;
                case FGFighterState.run:
                    velocity = new Vector2(facingLeft ? Mathf.Max(velocity.x - groundAcceleration, -maxGroundSpeed) : Mathf.Min(velocity.x + groundAcceleration, maxGroundSpeed), 0);
                    position = new Vector2(position.x + velocity.x, 0);

                    if(joystick.y == -1 && oldJoystick.y != -1)
                    {
                        state = FGFighterState.crouch;
                        CurrentAction = actions["crouch"];
                    }
                    else if((!facingLeft && joystick.x != 1) || (facingLeft && joystick.x != -1))
                    {
                        state = FGFighterState.idle;
                        CurrentAction = actions["idle"];
                    }
                    break;
            }

            currentAction.FGAUpdate(this);


            oldJoystick = joystick;
            oldPoke = poke;
            oldSpike = spike;
            oldLaunch = launch;
            oldJump = jump;
        }
    
        //This is a separate call/structure for netcode reasons.
        //Not that we're necessarily doing netcode, but it's better to be proactive
        public virtual void FGDraw()
        {
            currentAction.FGADraw(renderer);
        }
    
        public virtual void FGDrawHitboxes()
        {
            currentAction.FGADrawHitboxes(renderer);
        }

}

}