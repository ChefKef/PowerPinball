using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    private CircleCollider2D hitReg;
    public float elasticity = 5f; //How much rebound a shot will have when hitting the bumper.
    public float minimumLaunch = 50f; //The minimum amount of force with witch the ball will be launched after hitting the bumper
    // Start is called before the first frame update
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
            Vector3 ballPos = collision.gameObject.transform.position;
            Vector3 bumperPos = gameObject.transform.position;
            Vector2 ballDir = new Vector2(ballPos.x - bumperPos.x, ballPos.y - bumperPos.y);
            ballDir.Normalize();
            ballDir *= launchMagnitude;
            ballsManager.setVelocity(ballDir);
        }
    }
}
