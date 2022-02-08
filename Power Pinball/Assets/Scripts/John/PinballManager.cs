using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinballManager : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        applyForce(transform.up, 1000f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void applyForce(Vector3 direction, float force)
    {
        rb.AddForce(transform.up * 1000f);
    }

    public float movementMagnitue()
    {
        //Debug.Log("Velocity: " + rb.velocity.x + ", " + rb.velocity.y);
        //Debug.Log("Magnitude: " + rb.velocity.magnitude);
        return rb.velocity.magnitude;
    }

    public void setVelocity(Vector2 vel)
    {
        rb.velocity = vel;
    }

    //*****************DEBUG FUNCS******************
    private void forceTest()
    {
        Debug.Log("Applying upward force");
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * 1000f);
    }    
}
