using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rollover : MonoBehaviour
{
    public int points = 50;
    public float cooldown = .1f; //Time until the rollover can be activated again.
    private float counter;
    private bool active;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!active)
        {
            cooldown -= Time.deltaTime;
            if(cooldown <= 0)
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
}
