using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : MonoBehaviour
{
    public float minForce;
    public GameManager.RailType railType;
    public int player;

    //Private vars
    private Vector2[] points;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 childPoint;
        GameObject railParent;
        if(railType == GameManager.RailType.curved)
        {
            railParent = this.transform.parent.Find("CurvedRailPoints").gameObject;
        }
        else
        {
            railParent = this.transform.parent.Find("RailPoints").gameObject;
        }
        if(railParent)
        {
            points = new Vector2[railParent.transform.childCount];
        }
        for(int a = 0; a < points.Length; a++)
        {
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
        if (collision.gameObject.GetComponent<PinballManager>())
        {
            PinballManager ballsManager = collision.gameObject.GetComponent<PinballManager>();
            if(ballsManager.movementMagnitue() > minForce)
            {
                ballsManager.rideRail(points, railType, 1f);
            }
        }
    }
}
