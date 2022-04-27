using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomBall : PinballManager
{
    // Start is called before the first frame update
    private float lifetime = 3f;
    void Start()
    {
        //transform.GetComponent<CircleCollider2D>().isTrigger = true;
        transform.GetComponent<SpriteRenderer>().color = Color.grey;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(rb.bodyType == RigidbodyType2D.Static)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        */
        lifetime -= Time.deltaTime;
        if(lifetime <= 0)
        {
            Destroy(this.gameObject);
        }
        base.Update();
    }
}
