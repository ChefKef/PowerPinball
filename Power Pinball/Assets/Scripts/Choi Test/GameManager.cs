using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using FGScript;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Static Score property that persists between Scenes.
    /// </summary>
    public static int Score { get; private set; }

    //Game-Loop Data
    [SerializeField] public FGRenderer player1Renderer, player2Renderer;
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
        player1 = new Hipster(player1Renderer);
        player1Renderer.fighter = player1;
        //player2 = new Hipster();
        //player2Renderer.fighter = player2;

        player1.position.x = -6;
        //player2.position.x = 6;

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
        player1.FGDrawHitboxes();
        //player2.FGDraw();

    }
}
