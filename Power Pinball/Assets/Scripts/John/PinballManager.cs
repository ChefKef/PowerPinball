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
    private float holdTime;
    private float holdTimer;
    private Vector2 distanceToNextPoint;
    private bool holdBall = false;

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

        //DEBUG
    }

    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(holdBall)
        {
            if(holdTimer >= holdTime)
            {
                holdBall = false;
                toggleGravity(true);
                //gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
            else
            {
                holdTimer += Time.deltaTime;
            }
        }
        if(nextPoint >= 0)
        {
            railUpdate();
        }
    }

    public void applyForce(Vector3 direction, float force)
    {
        rb.AddForce(direction * force);
    }

    public float movementMagnitue()
    {
        //Debug.Log("Velocity: " + rb.velocity.x + ", " + rb.velocity.y);
        Debug.Log("Magnitude: " + rb.velocity.magnitude);
        return rb.velocity.magnitude;
    }

    public void setVelocity(Vector2 vel)
    {
        rb.velocity = vel;
    }

    public void setPos(Vector2 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }

    public int getPlayer()
    {
        return player;
    }

    public void holdBallForTime(float time)
    {
        holdBall = true;
        holdTimer = 0f;
        holdTime = time;
        toggleGravity(false);
        //gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic; //'Turn off' gravity
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
            toggleGravity(false);
            //gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic; //'Turn off' gravity
        }
        else
        {
            Debug.Log("Error: No rail points found.");
        }
    }

    public void moveToOverTime(Vector2 target, float elapsed)
    {
        transform.position = new Vector3(transform.position.x + (target.x * elapsed), transform.position.y + (target.y * elapsed), transform.position.z);
        //Debug.lo
        //Debug.Log("Ball position:")
    }

    private void railUpdate()
    {
        ballTravelTime += (Time.deltaTime * rampTimeCoefficient);
        ballTravelTime = ballTravelTime > 1f ? 1f : ballTravelTime;
        if (ballTravelTime >= 1f)
        {
            setPos(railPoints[nextPoint]);
            ballTravelTime = 0;
            previousTravelTime = 0;
            timeElapsed = 0;
            if (nextPoint + 1 < railPoints.Length)
            {
                nextPoint++;
                distanceToNextPoint = railPoints[nextPoint] - (Vector2)transform.position;
                //Debug.Log("Distance between points: (" + distanceToNextPoint.x + ", " + distanceToNextPoint.y + ")");
            }
            else
            {
                nextPoint = -1;
                toggleGravity(true);
                //gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic; //'Turn on' gravity
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
            timeElapsed = ballTravelTime - previousTravelTime;
            moveToOverTime(distanceToNextPoint, timeElapsed);
            previousTravelTime = ballTravelTime;
        }
    }

    public void toggleGravity(bool gravityOn)
    {
        if (gravityOn)
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
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
