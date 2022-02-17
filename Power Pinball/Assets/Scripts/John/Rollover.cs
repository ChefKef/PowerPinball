using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rollover : MonoBehaviour
{
    /// <summary>
    /// Base point value to be added to player's score when this component is
    /// interacted with.
    /// </summary>
    [SerializeField] private int points; // 50

    public float cooldown = .1f; //Time until the rollover can be activated again.
    private float counter;
    private bool active;
    // Start is called before the first frame update
    void Start()
    {
        counter = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if(!active)
        {
            counter -= Time.deltaTime;
            if(counter <= 0)
            {
                active = true;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(active)
        {
            if (collision.gameObject.GetComponent<PinballManager>())
            {
                PinballManager ballsManager = collision.gameObject.GetComponent<PinballManager>();
                counter = cooldown;
                active = false;
                GameManager.issuePoints(points, ballsManager.player);
            }
        }
        
    }

    // OnTriggerExit used because Rollovers are Triggers in the scene.
    // Alternatively, OnTriggernEnter can also be used.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (active)
        {
            if (collision.gameObject.GetComponent<PinballManager>())
            {
                PinballManager ballsManager = collision.gameObject.GetComponent<PinballManager>();
                counter = cooldown;
                active = false;

                // Update player score.
                GameManager.issuePoints(points, ballsManager.player);
            }
        }
    }
}
