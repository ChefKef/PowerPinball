using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinballManager : MonoBehaviour
{
    //Private vars
    private Rigidbody2D rb;
    private Vector2[] railPoints;
    private int nextPoint = -1;
    private float ballTravelTime = 0f;
    private Vector2 distanceToNextPoint;

    //Public vars
    public int player = 1;
    public bool curvedRail = false;
    public bool steepRail = false;
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
        if(nextPoint > 0)
        {
            ballTravelTime += (Time.deltaTime * 6f);
            ballTravelTime = ballTravelTime > 1f ? 1f : ballTravelTime;
            transform.position = new Vector3(transform.position.x + (distanceToNextPoint.x / ballTravelTime), transform.position.y + (distanceToNextPoint.y / ballTravelTime), transform.position.z);
            if(ballTravelTime >= 1f)
            {
                ballTravelTime = 0;
                if(nextPoint + 1 < railPoints.Length)
                {
                    nextPoint++;
                }
                else
                {
                    nextPoint = -1;
                    if (curvedRail) //Change to a switch statement if more ramps get introduced.
                    {
                        //Issue points and update game state for curved rail here.
                    }
                    else
                    {
                        //Issue points and update game state for ramp rail here.
                    }

                }
            }
        }
    }

    public void applyForce(Vector3 direction, float force)
    {
        rb.AddForce(direction * force);
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

    public int getPlayer()
    {
        return player;
    }

    public void rideRail(Vector2[] points, GameManager.RailType rail)
    {
        if(points.Length > 0)
        {
            railPoints = points;
            nextPoint = 0;
            distanceToNextPoint = points[0] - (Vector2)transform.position;
            if (rail == GameManager.RailType.curved) //Change to a switch statement if more ramps get introduced.
            {
                curvedRail = true;
            }
            else
            {
                steepRail = false;
            }
        }
        else
        {
            Debug.Log("Error: No rail points found.");
        }
    }

    //*****************DEBUG FUNCS******************
    private void forceTest()
    {
        Debug.Log("Applying upward force");
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * 1000f);
    } 
    
    
}
