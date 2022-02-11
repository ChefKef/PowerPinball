using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FGScript {

//ALL SIZES MEASURED IN UNITY UNITS (FOR NOW)

    public class FGHurtbox
    {
        public Rect rect;

        public FGHurtbox(Rect rect)
        {
            this.rect = rect;
        }

    }

    public class FGHitbox
    {
        public Rect rect;
        public Vector2 velocity; //where the ball is getting sent

        public FGHitbox(Rect rect, Vector2 vel)
        {
            this.rect = rect;
            this.velocity = vel;
        }

    }

}