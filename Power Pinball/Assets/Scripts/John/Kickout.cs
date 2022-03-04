using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kickout : MonoBehaviour
{
    public float holdTime = 0.5f; //Time, in seconds, that the ball will stay in place.
    public float inhaleTime = 0.1f; //Time, in seconds, that it will take for the ball to get to the center of the kickout.
    public float kickoutVelocity;

    private bool gettingBall = false;
    private bool holdingBall = false;
    private bool active = true;
    private float timer;
    private float inactivityTime = 3f;
    private PinballManager ballsManager;
    private Vector2 targetVector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gettingBall)
        {
            if(timer > inhaleTime)
            {
                ballsManager.setPos(transform.position);
                ballsManager.holdBallForTime(holdTime);
                gettingBall = false;
                holdingBall = true;
                timer = 0f;
            }
            else
            {
                ballsManager.moveToOverTime(targetVector, Time.deltaTime / inhaleTime);
                timer += Time.deltaTime;
            }
        }
        if(holdingBall)
        {
            if(timer >= holdTime)
            {
                holdingBall = false;
                kickout();
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        if(!active)
        {
            timer += Time.deltaTime;
            if(timer >= inactivityTime)
            {
                timer = 0f;
                active = true;
            }
        }
    }

    private void kickout()
    {
        active = false;
        timer = 0f;
        Debug.Log("Launching ball!");
        ballsManager.toggleGravity(true);
        ballsManager.applyForce(transform.up, kickoutVelocity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(active)
        {
            if (collision.gameObject.GetComponent<PinballManager>())
            {
                gettingBall = true;
                timer = 0f;
                ballsManager = collision.gameObject.GetComponent<PinballManager>();
                targetVector = transform.position - ballsManager.transform.position;
                ballsManager.toggleGravity(false);
            }
        }
    }
}
