using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    private CircleCollider2D hitReg;
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
            Debug.Log("yeah that works");
        }
    }
}
