using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public float baseSpinTime = .5f; //The time, in ms, that it takes for the spinner to spin at its slowest speed. This time is reduced via multiplier when the ball hits the spinner with a greater speed.
    public int basePoints = 25;
    public float magnitudeIncrement = 5f; //Example: if the ball hit the spinner with a magnitude of 20, the score multiplier would be 4.
                                            //Multiplier is truncated, so a magnitude of 24 would still result in a 4 multiplier.
    int maxMultiplier = 25;
    float scoreMult;
    float timer = 0f;
    PinballManager ballsManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(scoreMult > 0)
        {
            timer += Time.deltaTime;
            if(timer > baseSpinTime / ((scoreMult) / 1.5f))
            {
                scoreMult--;
                Debug.Log("Issuing score, timer is at: " + timer + ", number of issues left: " + scoreMult);
                timer = 0f;
                // Update player score.
                GameManager.issuePoints(basePoints * (int)scoreMult, ballsManager.player);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision.");
        if (collision.gameObject.GetComponent<PinballManager>())
        {
            Debug.Log("Ball found!");
            ballsManager = collision.gameObject.GetComponent<PinballManager>();
            scoreMult = (int)ballsManager.movementMagnitue() / (int)magnitudeIncrement; //Cast to an int to truncate.
            scoreMult = scoreMult > maxMultiplier ? maxMultiplier : scoreMult;
        }
    }

}
