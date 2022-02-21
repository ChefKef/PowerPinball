using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    /// <summary>
    /// Base point value to be added to player's score when this component is
    /// interacted with.
    /// </summary>
    [SerializeField] private int points;

    private CircleCollider2D hitReg;
    public float elasticity = 5f; //How much rebound a shot will have when hitting the bumper.
    public float minimumLaunch = 50f; //The minimum amount of force with witch the ball will be launched after hitting the bumper
    public float maximumLaunch = 600f; //Used to keep the ball from clipping out of bounds. Change only with exstensive testing. Going over 600 is asking for trouble.
    void Start()
    {
        hitReg = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PinballManager>())
        {
            PinballManager ballsManager = collision.gameObject.GetComponent<PinballManager>();
            float launchMagnitude = ballsManager.movementMagnitue();
            launchMagnitude *= elasticity;
            if (launchMagnitude < minimumLaunch)
            {
                launchMagnitude = minimumLaunch;
            }
            if(launchMagnitude > maximumLaunch)
            {
                launchMagnitude = maximumLaunch;
            }
            Debug.Log("Launch magnitude: " + launchMagnitude);
            Vector3 ballPos = collision.gameObject.transform.position;
            Vector3 bumperPos = gameObject.transform.position;
            Vector2 ballDir = new Vector2(ballPos.x - bumperPos.x, ballPos.y - bumperPos.y);
            ballDir.Normalize();
            ballDir *= launchMagnitude;
            ballsManager.setVelocity(ballDir);

            // Update player score.
            GameManager.issuePoints(points, ballsManager.player);
        }
    }
}
