using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FGScript {

public class FGRenderer : MonoBehaviour
{

        [SerializeField] private GameObject whitePixel;
        [SerializeField] private GameObject collisionChecker;

        private List<SpriteRenderer> hurtboxPool;
        private List<SpriteRenderer> hitboxPool;
        private List<BoxCollider2D> hitdetectPool;
        private List<Collider2D> collisions;

        public FGFighter fighter;

        private float scale = 4;


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
            hitdetectPool = new List<BoxCollider2D>();
            collisions = new List<Collider2D>();
        }


        public void CheckCollision()
        {
            FGAction action = fighter.CurrentAction;

            bool hit = action.CurrentHit == null ? false : true;
            int hitDiff = !hit ? 0 : hitdetectPool.Count - action.CurrentHit.Length;

            if (hitDiff < 0)
            {
                for (int i = hitDiff; i < 0; i++)
                {
                    GameObject go = Instantiate(collisionChecker);
                    hitdetectPool.Add(go.GetComponent<BoxCollider2D>());
                    go.transform.SetParent(this.transform);
                }
            }

            if (hit)
            {
                for (int i = 0; i < hitdetectPool.Count; i++)
                {
                    if (i < action.CurrentHit.Length)
                    {
                        hitdetectPool[i].enabled = true;
                        hitdetectPool[i].transform.position = new Vector3(fighter.position.x * scale + action.CurrentHit[i].rect.x * (fighter.facingLeft ? -1 : 1) * scale, fighter.position.y * scale + action.CurrentHit[i].rect.y * scale, -5);
                        hitdetectPool[i].transform.localScale = new Vector3(action.CurrentHit[i].rect.width * (fighter.facingLeft ? -1 : 1), action.CurrentHit[i].rect.height, 1) * 100 * scale;

                    }
                    else
                    {
                        hitdetectPool[i].enabled = false;
                    }
                }

                for (int i = 0; i < action.CurrentHit.Length; i++)
                {
                    if(hitdetectPool[i].IsTouchingLayers(7) && !fighter.hit)
                    {
                        //Debug.Log("Collision");
                        ContactFilter2D filter = new ContactFilter2D();
                        filter.SetLayerMask(7);
                        collisions.Clear();
                        hitdetectPool[i].OverlapCollider(filter, collisions);
                        collisions[0].attachedRigidbody.velocity = new Vector2(action.CurrentHit[i].velocity.x * (fighter.facingLeft ? -1 : 1), action.CurrentHit[i].velocity.y);

                        fighter.hit = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < hitboxPool.Count; i++)
                {
                    hitdetectPool[i].enabled = false;
                }
            }

        }

        public void DrawCollision(FGAction action)
        {


            //First, populate the Pools with objects we can draw, if we don't have enough.
            //Or, if we have too many, hide some

            //Positive means we have enough/too many. Negative means make more.
            bool hurt = action.CurrentHurt == null ? false : true;
            bool hit = action.CurrentHit == null ? false : true;
            int hurtDiff = !hurt ? 0 : hurtboxPool.Count - action.CurrentHurt.Length;
            int hitDiff = !hit ? 0 : hitboxPool.Count - action.CurrentHit.Length;

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
                for (int i = hitDiff; i < 0; i++)
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
                    if(i < action.CurrentHurt.Length)
                    {
                        hurtboxPool[i].enabled = true;
                        hurtboxPool[i].color = new Color(0, 0, 1, 0.5f);
                        hurtboxPool[i].transform.position = new Vector3(fighter.position.x * scale + action.CurrentHurt[i].rect.x * (fighter.facingLeft ? -1 : 1) * scale, fighter.position.y * scale + action.CurrentHurt[i].rect.y * scale, -5);
                        hurtboxPool[i].transform.localScale = new Vector3(action.CurrentHurt[i].rect.width * (fighter.facingLeft ? -1 : 1), action.CurrentHurt[i].rect.height, 1) * 100 * scale;

                    }
                    else
                    {
                        hurtboxPool[i].enabled = false;
                    }
                }
            else
            {
                for (int i = 0; i < hurtboxPool.Count; i++)
                {
                    hurtboxPool[i].enabled = false;
                }
            }
            if(hit)
                for (int i = 0; i < hitboxPool.Count; i++)
                {
                    if (i < action.CurrentHit.Length)
                    {
                        hitboxPool[i].enabled = true;
                        hitboxPool[i].color = new Color(1, 0, 0, 0.5f);
                        hitboxPool[i].transform.position = new Vector3(fighter.position.x * scale + action.CurrentHit[i].rect.x * (fighter.facingLeft ? -1 : 1) * scale, fighter.position.y * scale + action.CurrentHit[i].rect.y * scale, -5);
                        hitboxPool[i].transform.localScale = new Vector3(action.CurrentHit[i].rect.width * (fighter.facingLeft ? -1 : 1), action.CurrentHit[i].rect.height, 1) * 100 * scale;

                    }
                    else
                    {
                        hitboxPool[i].enabled = false;
                    }
                }
            else
            {
                for (int i = 0; i < hitboxPool.Count; i++)
                {
                    hitboxPool[i].enabled = false;
                }
            }

        }



}

}