using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : MonoBehaviour
{
    public float minForce;
    public GameManager.RailType railType;

    //Private vars
    private Vector2[] points;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 childPoint;
        GameObject railParent = GameObject.Find("RailPoints");
        if(railParent)
        {
            Debug.Log("PARENT FOUND!");
            points = new Vector2[railParent.transform.childCount];
            Debug.Log("Number of points: " + railParent.transform.childCount);
        }
        for(int a = 0; a < points.Length; a++)
        {
            Debug.Log("Creating point " + a);
            childPoint = railParent.transform.GetChild(a).position;
            points[a] = new Vector2(childPoint.x, childPoint.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Ramp collision hit!");
        if (collision.gameObject.GetComponent<PinballManager>())
        {
            PinballManager ballsManager = collision.gameObject.GetComponent<PinballManager>();
            if(ballsManager.movementMagnitue() > minForce)
            {
                ballsManager.rideRail(points, railType, 1f);
                ballsManager.setVelocity(Vector2.zero);
            }
        }
    }
}
