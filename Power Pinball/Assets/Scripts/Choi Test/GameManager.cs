using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Static Score property that persists between Scenes.
    /// </summary>
    public static int scoreP1 { get; private set; }
    public static int scoreP2 { get; private set; }

    //Game-Loop Data
    FGFighter player1, player2;

    
    public void GetPoints()
    {
        scoreP1++;
    }

    public static void Reset()
    {
        scoreP1 = 0;
    }

    void Start()
    {
        Time.fixedDeltaTime = 1f / 60.0f; //enforce 60 FPS

        scoreP1 = 0;

        //Character initialization
        //Specifically called in Start() not Awake() to wait for any data to get passed in from hypothetical singleton
        //player1 = new Hipster(); commenting out so that everything compiles - John
        //player2 = new Hipster();

    }

    //Updates at a fixed rate, as opposed to Update() which is reliant on the rendering pipeline.
    //Set to 60FPS in Start()
    void FixedUpdate() 
    {
        player1.FGUpdate();
        //player2.FGUpdate();

        /*
         * 
         *  HANDLE COLLISIONS HERE (probably just a function call..?)
         * 
         */

        player1.FGDraw();
        //player2.FGDraw();

    }

    public static void issuePoints(int points, int player = 1)
    {
        if (player == 1)
        {
            scoreP1 += points;
        }
        if (player == 2)
        {
            scoreP2 += points;
        }
    }
}
