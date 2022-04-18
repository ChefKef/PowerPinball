using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockdowns : MonoBehaviour
{
    public bool knockedDown = false;

    private GameObject hitbox;
    private GameObject indicator;
    private Sprite activated;
    private Sprite hittable;
    private bool active = true;
    private float activeTimer = .5f;
    // Start is called before the first frame update
    void Start()
    {
        hitbox = gameObject.transform.GetChild(0).gameObject;
        indicator = gameObject.transform.GetChild(1).gameObject;
        activated = Resources.Load<Sprite>("DropTargetInactive");
        hittable = Resources.Load<Sprite>("DropTargetActive");
    }

    // Update is called once per frame
    void Update()
    {
        if(!active)
        {
            activeTimer -= Time.deltaTime;
            if(activeTimer <= 0)
            {
                active = true;
                activeTimer = .5f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (active && !knockedDown && collision.gameObject.GetComponent<PinballManager>())
        {
            knockedDown = true;
            hitbox.GetComponent<SpriteRenderer>().color = Color.gray;
            indicator.GetComponent<SpriteRenderer>().sprite = activated;
        }
    }

    public void ResetKnockdown()
    {
        knockedDown = false;
        hitbox.GetComponent<SpriteRenderer>().color = Color.white;
        indicator.GetComponent<SpriteRenderer>().sprite = hittable;
        active = false;
    }
}
