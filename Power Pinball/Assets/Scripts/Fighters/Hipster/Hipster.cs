using FGScript;

public class Hipster : FGFighter
{

    //In this class I'm going to initialize all the hitbox/hurtbox data
    //This is cringe I'm just too lazy to do File IO in the next ~3 days

    public Hipster(FGRenderer renderer) : base (renderer)
    {
        actions["idle"] = FGAction.newDefaultAction();


        currentAction = actions["idle"];
        state = FGFighterState.idle;

    }

}
