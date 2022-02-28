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
    private float previousTravelTime = 0f;
    private float timeElapsed = 0f;
    private float rampTimeCoefficient;
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
        if(nextPoint >= 0)
        {
            ballTravelTime += (Time.deltaTime * rampTimeCoefficient);
            ballTravelTime = ballTravelTime > 1f ? 1f : ballTravelTime;
            if(ballTravelTime >= 1f)
            {
                transform.position = new Vector3(railPoints[nextPoint].x, railPoints[nextPoint].y, transform.position.z);
                //Debug.Log("Ball position after full travel time: (" + transform.position.x + ", " + transform.position.y + ")");
                ballTravelTime = 0;
                previousTravelTime = 0;
                timeElapsed = 0;
                if(nextPoint + 1 < railPoints.Length)
                {
                    nextPoint++;
                    distanceToNextPoint = railPoints[nextPoint] - (Vector2)transform.position;
                    //Debug.Log("Distance between points: (" + distanceToNextPoint.x + ", " + distanceToNextPoint.y + ")");
                }
                else
                {
                    nextPoint = -1;
                    gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic; //'Turn on' gravity
                    if (curvedRail) //Change to a switch statement if more ramps get introduced.
                    {
                        //Issue points and update game state for curved rail here.
                        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
                    }
                    else
                    {
                        //Issue points and update game state for ramp rail here.
                        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
                    }

                }
            }
            else
            {
                //Debug.Log("timeElapsed: " + timeElapsed + ", ballTravelTime: " + ballTravelTime);
                timeElapsed = ballTravelTime - previousTravelTime;
                transform.position = new Vector3(transform.position.x + (distanceToNextPoint.x * timeElapsed), transform.position.y + (distanceToNextPoint.y * timeElapsed), transform.position.z);
                //Debug.Log("Ball position mid travel: (" + transform.position.x + ", " + transform.position.y + ") Time traveled so far: " + ballTravelTime);
                previousTravelTime = ballTravelTime;
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

    public void rideRail(Vector2[] points, GameManager.RailType rail, float totalAnimationTime)
    {
        if(points.Length > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -6);
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
            rampTimeCoefficient = points.Length / totalAnimationTime;
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
