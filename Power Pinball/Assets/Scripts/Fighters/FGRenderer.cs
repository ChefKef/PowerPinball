using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FGScript {

public class FGRenderer : MonoBehaviour
{

        [SerializeField] private GameObject whitePixel;
        [SerializeField] private GameObject spriteRenderer;
        [SerializeField] private GameObject collisionChecker;

        private List<SpriteRenderer> hurtboxPool;
        private List<SpriteRenderer> hitboxPool;
        private List<SpriteRenderer> spritePool;
        private List<BoxCollider2D> hitdetectPool;
        private List<Collider2D> collisions;

        public GameObject comboCounter;
        public FGFighter fighter;

        private float scale = 8;
        private float spriteScale = 0.5f;
        private ComboCounter comboCounterScript;

        public FGHitbox hitDetected;

        private bool spiked = false;

        public AudioController AudioControllerScript { get; private set; }

        [SerializeField] private int playerNumber;

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
            spritePool = new List<SpriteRenderer>();
            hitdetectPool = new List<BoxCollider2D>();
            collisions = new List<Collider2D>();
            comboCounterScript = comboCounter.GetComponent<ComboCounter>();
            AudioControllerScript = FindObjectOfType<AudioController>();
        }

        private void FixedUpdate()
        {
            if (fighter.comboCount <= 0) spiked = false;
        }

        public int CheckCollision()
        {
            FGAction action = fighter.CurrentAction;

            bool hit = action.CurrentHit == null ? false : true;
            int hitDiff = !hit ? 0 : hitdetectPool.Count - action.CurrentHit.Length;

            //This is just resource pooling- making exactly as many colliders as we need, no more.
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
                        hitdetectPool[i].transform.position = new Vector3(fighter.position.x * scale + action.CurrentHit[i].rect.x * (fighter.facingLeft ? -1 : 1) * scale, fighter.position.y * scale + action.CurrentHit[i].rect.y * scale, -5) + this.transform.position;
                        hitdetectPool[i].transform.localScale = new Vector3(action.CurrentHit[i].rect.width * (fighter.facingLeft ? -1 : 1), action.CurrentHit[i].rect.height, 1) * 100 * scale;

                    }
                    else
                    {
                        hitdetectPool[i].enabled = false;
                    }
                }

                //This is where collision actually happens.
                for (int i = 0; i < action.CurrentHit.Length; i++)
                {
                    if(hitdetectPool[i].IsTouchingLayers(7) && !fighter.hit)
                    {
                        //Debug.Log("Collision");
                        ContactFilter2D filter = new ContactFilter2D();
                        filter.SetLayerMask(7);
                        collisions.Clear();
                        hitdetectPool[i].OverlapCollider(filter, collisions);

                        //This threw an error once idk why

                        fighter.hit = true;
                        fighter.Hitstop = true;

                        int hitstop = Mathf.Min(Mathf.Max(3, (int)(action.CurrentHit[i].velocity.magnitude / 3.7f)), 23);

                        hitDetected = action.CurrentHit[i];

                        // Play SFX.
                        AudioControllerScript.PlayAudio(AudioClips.Impact);

                        fighter.comboCount++;
                        if (!comboCounter.activeInHierarchy) comboCounter.SetActive(true);
                        comboCounterScript.SetText(fighter.comboCount);
                        //Debug.Log("Combo: " + fighter.comboCount + " hits!");

                        if (action.CurrentHit[i].velocity.y < 0) spiked = true;

                        if (hitstop > 0) return hitstop;
                        Vector2 ballDI = new Vector2(fighter.Joystick.x, 0) * 0.2f;

                        collisions[0].attachedRigidbody.velocity = new Vector2(action.CurrentHit[i].velocity.x * (fighter.facingLeft ? -1 : 1), action.CurrentHit[i].velocity.y) + (ballDI * action.CurrentHit[i].velocity.magnitude);

                        return hitstop;

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

            return 0;

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
                        hurtboxPool[i].transform.position = new Vector3(fighter.position.x * scale + action.CurrentHurt[i].rect.x * (fighter.facingLeft ? -1 : 1) * scale, fighter.position.y * scale + action.CurrentHurt[i].rect.y * scale, -5) + this.transform.position;
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
                        hitboxPool[i].transform.position = new Vector3(fighter.position.x * scale + action.CurrentHit[i].rect.x * (fighter.facingLeft ? -1 : 1) * scale, fighter.position.y * scale + action.CurrentHit[i].rect.y * scale, -5) + this.transform.position;
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

        public void Draw(FGAction action)
        {
            bool sprite = action.CurrentSprite == null ? false : true;
            int spriteDiff = !sprite ? 0 : spritePool.Count - action.CurrentSprite.Length;

            if(spriteDiff < 0)
            {
                for (int i = spriteDiff; i < 0; i++)
                {
                    GameObject go = Instantiate(spriteRenderer);
                    spritePool.Add(go.GetComponent<SpriteRenderer>());
                    go.transform.SetParent(this.transform);
                }
            }

            //Actually draw
            if(sprite)
                for (int i = 0; i < spritePool.Count; i++)
                {
                    Customisables customisableComponent;
                    if(i < action.CurrentSprite.Length)
                    {
                        spritePool[i].enabled = true;
                        spritePool[i].sprite = action.CurrentSprite[i];
                        if(fighter.facingLeft) spritePool[i].flipX = true;
                        else spritePool[i].flipX = false;

                        switch (playerNumber)
                        {
                            case 1:
                                if (Enum.TryParse<Customisables>(action.CurrentSprite[i].name, true, out customisableComponent))
                                    spritePool[i].color = ColourProfileManager.p1ColourProfile.profile[customisableComponent];
                                break;
                            case 2:
                                if (Enum.TryParse<Customisables>(action.CurrentSprite[i].name, true, out customisableComponent))
                                    spritePool[i].color = ColourProfileManager.p2ColourProfile.profile[customisableComponent];
                                break;
                        }

                        
                                //0, 1 / (i + 0.01f), 0.2f * i, 1);
                        //action.CurrentSprite[i].name
                        spritePool[i].transform.position = new Vector3(fighter.position.x * scale + (-spritePool[i].bounds.size.x/2 + action.spriteOffset.x) * (fighter.facingLeft ? -1 : 1), fighter.position.y * scale + spritePool[i].bounds.size.y, 0 - 0.1f*i) + this.transform.position;
                        spritePool[i].transform.localScale = new Vector3(1,1,1) * spriteScale;

                    }
                    else
                    {
                        spritePool[i].enabled = false;
                    }
                }
            else
            {
                for (int i = 0; i < spritePool.Count; i++)
                {
                    spritePool[i].enabled = false;
                }
            }

        }

        private void Update()
        {
            // If visible, make the combo counter follow the player sprite.
            if (comboCounter.activeInHierarchy)
                comboCounterScript.SetPosition(spritePool[0].transform.position);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.attachedRigidbody.velocity.y > 0) return;
            if (!spiked)
            {
                fighter.comboCount = 0;
                this.comboCounter.SetActive(false);
                collision.attachedRigidbody.velocity = new Vector2(collision.attachedRigidbody.velocity.x, -collision.attachedRigidbody.velocity.y) * 0.5f;
            }
            else
            {
                collision.attachedRigidbody.velocity = new Vector2(collision.attachedRigidbody.velocity.x, -collision.attachedRigidbody.velocity.y);
                spiked = false;
            }
        }

    }

}