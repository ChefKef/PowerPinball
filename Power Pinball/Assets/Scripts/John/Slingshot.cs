using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slingshot : MonoBehaviour
{
    /// <summary>
    /// Base point value to be added to player's score when this component is
    /// interacted with.
    /// </summary>
    [SerializeField] private int points;

    [SerializeField] private GameObject ui;
    
    private EdgeCollider2D hitReg;
    public float elasticity = 2.5f; //How much rebound a shot will have when hitting the slingshot.
    public float minimumLaunch = 30f; //The minimum amount of force with which the ball will be launched after hitting the slingshot.
    public float angleManipulation = 1.2f; //How much the angle is corrected. Closer to 1 is full angle correction, higher number = wilder launches.
    private bool isFlipped = false;
    void Start()
    {
        hitReg = GetComponent<EdgeCollider2D>();
        if (transform.localScale.y < 0) //If slingshot is flipped, mark it as so, and alter it's up vector accordingly.
        {
            isFlipped = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PinballManager>() && hitReg.IsTouching(collision.gameObject.GetComponent<CircleCollider2D>()))
        {
            PinballManager ballsManager = collision.gameObject.GetComponent<PinballManager>();
            float launchMagnitude = ballsManager.movementMagnitue();
            launchMagnitude *= elasticity;
            if (launchMagnitude < minimumLaunch)
            {
                launchMagnitude = minimumLaunch;
            }
            Vector2 collisionPos = collision.contacts[0].point;
            Vector2 centerPos = gameObject.transform.position;
            Vector2 ballDir = new Vector2(collisionPos.x - centerPos.x, collisionPos.y - centerPos.y);
            ballDir.Normalize();
            Debug.Log("Launch direction:" + ballDir.x + ", " + ballDir.y);
            Debug.Log("Transform up: " + transform.up);
            Vector2 slingshotUp = transform.up.normalized;
            if(isFlipped)
            {
                slingshotUp.x = -slingshotUp.x;
                slingshotUp.y = -slingshotUp.y;
            }
            ballDir = new Vector2(ballDir.x + (slingshotUp.x - ballDir.x) / angleManipulation, ballDir.y + (slingshotUp.y - ballDir.y) / angleManipulation);
            ballDir *= launchMagnitude;
            ballsManager.setVelocity(ballDir);

            // Update player score.
            GameManager.issuePoints(points, ballsManager.player);

            // Request a TMPro object from the object pool and parent it to the
            // UIManager GameObject in the Hierarchy.
            GameObject pooledObject = HitScoreObjectPool.Instance.GetPooledObject();
            pooledObject.transform.SetParent(ui.transform);

            // Modify the text values so they correspond to the component's
            // value and position onscreen.
            HitScore hitScore = pooledObject.GetComponent<HitScore>();
            hitScore.SetText(points.ToString());
            hitScore.SetPosition(transform.position);
        }
    }
}
