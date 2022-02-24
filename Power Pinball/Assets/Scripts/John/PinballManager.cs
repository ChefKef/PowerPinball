using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinballManager : MonoBehaviour
{
    Rigidbody2D rb;
    public int player = 1;
    [SerializeField] private Vector2 startPos; // (33, 129)

    /// <summary>
    /// Resets pinball physics.
    /// </summary>
    public void Init()
    {
        // Move pinball to spawn location.
        transform.position = startPos;

        rb = GetComponent<Rigidbody2D>();
        
        // Reset linear/angular velocity AND rotation.
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        transform.rotation = Quaternion.Euler(Vector2.zero);

        // Launch ball upwards.
        applyForce(transform.up, 1000f);
    }

    void Start()
    {
        Init();
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
    
    public int getPlayer()
    {
        return player;
    }
}
