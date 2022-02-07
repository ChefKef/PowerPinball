using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Static Score property that persists between Scenes.
    /// </summary>
    public static int Score { get; private set; }

    //Game-Loop Data
    FGFighter player1, player2;

    
    public void GetPoints()
    {
        Score++;
    }

    public static void Reset()
    {
        Score = 0;
    }

    void Start()
    {
        Time.fixedDeltaTime = 1f / 60.0f; //enforce 60 FPS

        Score = 0;

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
}
