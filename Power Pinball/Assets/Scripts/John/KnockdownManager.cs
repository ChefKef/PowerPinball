using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockdownManager : MonoBehaviour
{
    public float baseMultiplierIncrease;
    public int player;

    private int childrenCount;
    private bool allDown = false;
    // Start is called before the first frame update
    void Start()
    {
        childrenCount = transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        allDown = true;
        for(int a = 0; a < childrenCount; a++)
        {
            if(!transform.GetChild(a).GetComponent<Knockdowns>().knockedDown)
            {
                allDown = false;
                break;
            }
        }
        if(allDown)
        {
            Debug.Log("All knockdowns are down!");
            for(int b = 0; b < childrenCount; b++)
            {
                transform.GetChild(b).GetComponent<Knockdowns>().ResetKnockdown();
            }
            GameManager.issuePoints(500, player);
            if (player == 1)
            {
                GameManager.multiplierP1 += baseMultiplierIncrease;
                baseMultiplierIncrease *= .9f;
            }
            else
            {
                GameManager.multiplierP2 += baseMultiplierIncrease;
                baseMultiplierIncrease *= .9f;
            }
        }
    }
}
