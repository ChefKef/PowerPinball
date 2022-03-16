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
        protected const float groundLocationY = -17.62f; //TODO don't fucking do this
        protected const float wallLocationX = 11; //mirrored
    
        //Serialized Data
        protected Dictionary<string, FGAction> actions;
        protected float maxGroundSpeed;
        protected float maxAirSpeed; //horizontal
        protected float groundAcceleration;
        protected float friction;
        protected float airAcceleration;
        protected float gravity;
        protected float jumpVelocity;
    
        //State data
        public Vector2 position, velocity; //coordinate system: x horizontal y vertical, positive is up & right
        public FGFighterState state = FGFighterState.idle; //this is useful as a separate concept from currentAction, so we can do things like handle air drift the same regardless of if attacking or not.
        private FGAction _currentAction;
        protected int cFrame; //current frame; how far along we are in a given action.
        protected int hitstunFrames; //if > 0, we skip updating that frame for visual/gamefeel reasons
        public bool hit; //generally: whether the attack we used connected. Set to true when it's possible to cancel into an action
        private bool _hitstop;
        public bool facingLeft;
    
        //Passthroughs
        public FGAction CurrentAction {
            get => _currentAction;
            set
            {
                _currentAction = value;
                _currentAction.SetActive();
            }
        
        }
        //This is for buffering things out of hitstop. I know, it's gross.
        public bool Hitstop
        {
            get => _hitstop;
            set => _hitstop = value;
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
        //Hitstop buffer
        private bool bffPoke;
        private bool bffSpike;
        private bool bffLaunch;
        private bool bffJump;

        public Vector2 Joystick { get => joystick; }

        //Button callbacks
#region Input
        public void MoveStick(Vector2 stick)
        {
            if (stick.x == 0) joystick.x = 0;
            else if (stick.x > 0) joystick.x = 1;
            else joystick.x = -1;

            if (stick.y == 0) joystick.y = 0;
            else if (stick.y > 0) joystick.y = 1;
            else joystick.y = -1;
        }
        public void PokeBtn(bool btn)
        {
            poke = btn;
            if (btn && Hitstop) bffPoke = true;
        }
        public void SpikeBtn(bool btn)
        {
            spike = btn;
            if (btn && Hitstop) bffSpike = true;
        }
        public void LaunchBtn(bool btn)
        {
            launch = btn;
            if (btn && Hitstop) bffLaunch = true;
        }
        public void JumpBtn(bool btn)
        {
            jump = btn;
            if (btn && Hitstop) bffJump = true;
        }
#endregion

        public FGFighter(FGRenderer renderer)
        {
            actions = new Dictionary<string, FGAction>();
            this.renderer = renderer;

            position = new Vector2(0, groundLocationY);
            velocity = new Vector2();

            //Saves some time in testing.
            actions["idle"] = FGAction.newDefaultAction(1);
            actions["run"] = FGAction.newDefaultAction(1);
            actions["crouch"] = FGAction.newDefaultAction(1);
            actions["air"] = FGAction.newDefaultAction(1);

            CurrentAction = actions["idle"];
            state = FGFighterState.idle;

        }
    
        public virtual void FGUpdate()
        {
            if(CurrentAction.ended)
            {
                if(state == FGFighterState.attack)
                {
                    state = FGFighterState.idle;
                    CurrentAction = actions["idle"];
                    hit = false;
                }
                else if(state == FGFighterState.airAttack)
                {
                    state = FGFighterState.air;
                    CurrentAction = actions["air"];
                    hit = false;
                }
            }

            
            switch (state)
            {
                case FGFighterState.idle:
                case FGFighterState.crouch:
                    position = new Vector2(position.x + velocity.x, groundLocationY);
                    velocity *= friction;

                    if (jump && !oldJump)
                    {
                        state = FGFighterState.air;
                        CurrentAction = actions["air"];
                        velocity.x = maxAirSpeed * joystick.x;
                        velocity.y = jumpVelocity;
                    }
                    else if((poke && !oldPoke) || bffPoke)
                    {
                        state = FGFighterState.attack;
                        CurrentAction = actions["poke"];
                    }
                    else if ((spike && !oldSpike) || bffSpike)
                    {
                        state = FGFighterState.attack;
                        CurrentAction = actions["spike"];
                    }
                    else if ((launch && !oldLaunch) || bffLaunch)
                    {
                        state = FGFighterState.attack;
                        CurrentAction = actions["launch"];
                    }
                    else if (joystick.x == 1 && joystick.y != -1)
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
                    velocity = new Vector2(facingLeft ? Mathf.Max(velocity.x - groundAcceleration, -maxGroundSpeed) : Mathf.Min(velocity.x + groundAcceleration, maxGroundSpeed), groundLocationY);
                    position = new Vector2(position.x + velocity.x, groundLocationY);

                    if (jump && !oldJump)
                    {
                        state = FGFighterState.air;
                        CurrentAction = actions["air"];
                        velocity.x = maxAirSpeed * joystick.x;
                        velocity.y = jumpVelocity;
                    }
                    else if ((poke && !oldPoke) || bffPoke)
                    {
                        state = FGFighterState.attack;
                        CurrentAction = actions["poke"];
                    }
                    else if ((spike && !oldSpike) || bffSpike)
                    {
                        state = FGFighterState.attack;
                        CurrentAction = actions["spike"];
                    }
                    else if ((launch && !oldLaunch) || bffLaunch)
                    {
                        state = FGFighterState.attack;
                        CurrentAction = actions["launch"];
                    }
                    else if (joystick.y == -1 && oldJoystick.y != -1)
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

                case FGFighterState.air:
                case FGFighterState.airAttack:
                    float vel = velocity.x + (joystick.x * airAcceleration);
                    if (vel > maxAirSpeed) vel = maxAirSpeed;
                    if (vel < -maxAirSpeed) vel = -maxAirSpeed;
                    velocity = new Vector2(vel, velocity.y - gravity);
                    position = new Vector2(position.x + velocity.x, position.y + velocity.y);

                    if(position.y <= groundLocationY)
                    {
                        state = FGFighterState.idle;
                        position.y = groundLocationY;
                        CurrentAction = actions["idle"];
                        hit = false;
                    }
                    else if (bffPoke || (poke && !oldPoke && (state == FGFighterState.air || hit)))
                    {
                        state = FGFighterState.airAttack;
                        CurrentAction = actions["airPoke"];
                        hit = false;
                    }
                    else if (bffSpike || (spike && !oldSpike && (state == FGFighterState.air || hit)))
                    {
                        state = FGFighterState.airAttack;
                        CurrentAction = actions["airSpike"];
                        hit = false;
                    }
                    else if (bffLaunch || (launch && !oldLaunch && (state == FGFighterState.air || hit)))
                    {
                        state = FGFighterState.airAttack;
                        CurrentAction = actions["airLaunch"];
                        hit = false;
                    }

                    break;
                case FGFighterState.attack:
                    if(hit)
                    {
                        if ((jump && !oldJump) || bffJump)
                        {
                            state = FGFighterState.air;
                            CurrentAction = actions["air"];
                            velocity.x = maxAirSpeed * joystick.x;
                            velocity.y = jumpVelocity;
                            hit = false;
                        }
                        else if ((poke && !oldPoke) || bffPoke)
                        {
                            state = FGFighterState.attack;
                            if(CurrentAction == actions["poke"])
                            {
                                CurrentAction = actions["autoCombo2"];
                            }
                            else if(CurrentAction == actions["autoCombo2"])
                            {
                                CurrentAction = actions["autoCombo3"];
                            }
                            else
                            {
                                CurrentAction = actions["poke"];
                            }
                            hit = false;
                        }
                        else if ((spike && !oldSpike) || bffSpike)
                        {
                            state = FGFighterState.attack;
                            CurrentAction = actions["spike"];
                            hit = false;
                        }
                        else if ((launch && !oldLaunch) || bffLaunch)
                        {
                            state = FGFighterState.attack;
                            CurrentAction = actions["launch"];
                            hit = false;
                        }
                    }
                    break;
            }

            CurrentAction.FGAUpdate(this);

            if(position.x > wallLocationX) position.x = wallLocationX;
            if(position.x < -wallLocationX) position.x = -wallLocationX;

            oldJoystick = joystick;
            oldJump = jump;
            oldPoke = poke;
            oldSpike = spike;
            oldLaunch = launch;
            if (bffJump || bffPoke || bffSpike || bffLaunch) bffPoke = bffSpike = bffLaunch = bffJump = false;
        }
    
        //This is a separate call/structure for netcode reasons.
        //Not that we're necessarily doing netcode, but it's better to be proactive
        public virtual void FGDraw()
        {
            CurrentAction.FGADraw(renderer);
        }
    
        public virtual void FGDrawHitboxes()
        {
            CurrentAction.FGADrawHitboxes(renderer);
        }

}

}