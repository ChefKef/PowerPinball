using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBumper : Bumper
{
    public float radius = 1f;
    public float period = 2f; //How long, in seconds, it takes the bumper to make a full rotation.

    private Vector3 anchor;
    private float timer = 0f;
    private float deg = 0f;
    // Start is called before the first frame update
    void Start()
    {
        anchor = transform.position;
        transform.position = new Vector3(anchor.x + (Mathf.Sin(Mathf.Deg2Rad * deg) * radius), anchor.y + (Mathf.Cos(Mathf.Deg2Rad * deg) * radius), anchor.z);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        deg = timer / period * 360f;
        transform.position = new Vector3(anchor.x + (Mathf.Sin(Mathf.Deg2Rad * deg) * radius), anchor.y + (Mathf.Cos(Mathf.Deg2Rad * deg) * radius), anchor.z);
    }
}
