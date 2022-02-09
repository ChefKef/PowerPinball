using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FGScript {

public class FGRenderer : MonoBehaviour
{

        [SerializeField] private GameObject whitePixel;

        private List<SpriteRenderer> hurtboxPool;
        private List<SpriteRenderer> hitboxPool;

        public FGFighter fighter;

        private float scale = 100;


        //We Need to "handle" Inputs here because this is the Unity object
        //In reality, we are passing them along to the Fighter
        #region Input

        public void OnMove(InputValue input)
        {
            fighter.MoveStick(input.Get<Vector2>());
        }
        public void OnJump(InputValue input)
        {
            fighter.JumpBtn(input.Get<float>() == 1);
        }
        public void OnPoke(InputValue input)
        {
            fighter.PokeBtn(input.Get<float>() == 1);
        }
        public void OnSpike(InputValue input)
        {
            fighter.SpikeBtn(input.Get<float>() == 1);
        }
        public void OnLaunch(InputValue input)
        {
            fighter.LaunchBtn(input.Get<float>() == 1);
        }

        #endregion

        void Awake()
        {
            hurtboxPool = new List<SpriteRenderer>();
            hitboxPool = new List<SpriteRenderer>();
        }


        public void DrawCollision(FGAction action)
        {


            //First, populate the Pools with objects we can draw, if we don't have enough.
            //Or, if we have too many, hide some

            //Positive means we have enough/too many. Negative means make more.
            bool hurt = action.currentHurt == null ? false : true;
            bool hit = action.currentHit == null ? false : true;
            int hurtDiff = !hurt ? 0 : hurtboxPool.Count - action.currentHurt.Length;
            int hitDiff = !hit ? 0 : hitboxPool.Count - action.currentHit.Length;

            if(hurtDiff < 0)
            {
                for (int i = hurtDiff; i < 0; i++)
                {
                    GameObject go = Instantiate(whitePixel);
                    hurtboxPool.Add(go.GetComponent<SpriteRenderer>());
                    go.transform.SetParent(this.transform);
                }
            }
            if (hitDiff < 0)
            {
                for (int i = hurtDiff; i < 0; i++)
                {
                    GameObject go = Instantiate(whitePixel);
                    hitboxPool.Add(go.GetComponent<SpriteRenderer>());
                    go.transform.SetParent(this.transform);
                }
            }

            //Actually draw
            if(hurt)
                for (int i = 0; i < hurtboxPool.Count; i++)
                {
                    if(i < action.currentHurt.Length)
                    {
                        hurtboxPool[i].enabled = true;
                        hurtboxPool[i].color = new Color(0, 0, 1, 0.5f);
                        hurtboxPool[i].transform.position = new Vector3(fighter.position.x + action.currentHurt[i].rect.x * (fighter.facingLeft ? -1 : 1), fighter.position.y + action.currentHurt[i].rect.y, 5);
                        hurtboxPool[i].transform.localScale = new Vector3(action.currentHurt[i].rect.width * (fighter.facingLeft ? -1 : 1), action.currentHurt[i].rect.height, 1) * scale;

                    }
                    else
                    {
                        hurtboxPool[i].enabled = false;
                    }
                }
            if(hit)
                for (int i = 0; i < hitboxPool.Count; i++)
                {
                    if (i < action.currentHit.Length)
                    {
                        hitboxPool[i].enabled = true;
                        hitboxPool[i].color = new Color(1, 0, 0, 0.5f);
                        hitboxPool[i].transform.position = new Vector3(fighter.position.x + action.currentHit[i].rect.x * (fighter.facingLeft ? -1 : 1), fighter.position.y + action.currentHit[i].rect.y, 5);
                        hitboxPool[i].transform.localScale = new Vector3(action.currentHit[i].rect.width * (fighter.facingLeft ? -1 : 1), action.currentHit[i].rect.height, 1) * scale;

                    }
                    else
                    {
                        hitboxPool[i].enabled = false;
                    }
                }

        }



}

}