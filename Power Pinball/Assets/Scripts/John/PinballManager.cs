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
    private float timeElapsed = 0f;
    private Vector2 distanceToNextPoint;

    //Public vars
    public int player = 1;
    public bool curvedRail = false;
    public bool steepRail = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        applyForce(transform.up, 100f);
    }

    // Update is called once per frame
    void Update()
    {
        if(nextPoint >= 0)
        {
            ballTravelTime += (Time.deltaTime * 6f);
            ballTravelTime = ballTravelTime > 1f ? 1f : ballTravelTime;
            if(ballTravelTime >= 1f)
            {
                transform.position = new Vector3(railPoints[nextPoint].x, railPoints[nextPoint].y, transform.position.z);
                Debug.Log("Ball position after full travel time: (" + transform.position.x + ", " + transform.position.y + ")");
                ballTravelTime = 0;
                timeElapsed = 0;
                if(nextPoint + 1 < railPoints.Length)
                {
                    nextPoint++;
                    distanceToNextPoint = railPoints[nextPoint] - (Vector2)transform.position;
                }
                else
                {
                    nextPoint = -1;
                    gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic; //'Turn on' gravity
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
            else
            {
                timeElapsed = ballTravelTime - timeElapsed;
                transform.position = new Vector3(transform.position.x + (distanceToNextPoint.x * timeElapsed), transform.position.y + (distanceToNextPoint.y * timeElapsed), transform.position.z);
                Debug.Log("Ball position mid travel: (" + transform.position.x + ", " + transform.position.y + ") Time traveled so far: " + ballTravelTime);
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
            Debug.Log("Number of points found: " + points.Length);
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
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic; //'Turn off' gravity
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
