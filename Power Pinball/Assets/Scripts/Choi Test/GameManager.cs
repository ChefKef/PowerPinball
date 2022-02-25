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
    public static int scoreP1 { get; private set; }
    public static int scoreP2 { get; private set; }

    public enum RailType
    {
        curved = 0,
        ramp = 1
    }

    //Game-Loop Data
    [SerializeField] public FGRenderer player1Renderer, player2Renderer;
    [SerializeField] private GameObject player1Pinball;
    FGFighter player1, player2;
    int p1Hitstop, p2Hitstop;

    //public void GetPoints()
    //{
    //    scoreP1++;
    //}

    [SerializeField] private GameObject ui;
    private UIManager uiManager;

    [SerializeField] private GameObject pinball;
    private PinballManager pinballManager;

    /// <summary>
    /// Initialisation method.
    /// </summary>
    private void Init()
    {
        uiManager = ui.GetComponent<UIManager>();
        pinballManager = pinball.GetComponent<PinballManager>();

        Time.fixedDeltaTime = 1f / 60.0f; //enforce 60 FPS

        scoreP1 = 0;

        //Character initialization
        //Specifically called in Start() not Awake() to wait for any data to get passed in from hypothetical singleton
        player1 = new Hipster(player1Renderer);
        player1Renderer.fighter = player1;
        player1.FGUpdate();
        player1.FGDraw();
        //player2 = new Hipster();
        //player2Renderer.fighter = player2;

        player1.position.x = -6;
        //player2.position.x = 6;

        // Wait until the countdown completes.
        Time.timeScale = 0;
    }

    void Start()
    {
        Init();
    }

    private void Update()
    {
        // Countdown finished. Run game.
        if (!UIManager.isCountingDown) Time.timeScale = 1;

        // Game over. Stop game.
        if (UIManager.gameOver) Time.timeScale = 0;
    }

    //Updates at a fixed rate, as opposed to Update() which is reliant on the rendering pipeline.
    //Set to 60FPS in Start()
    void FixedUpdate() 
    {
        if (p1Hitstop <= 0)
        {
            player1.FGUpdate();
            p1Hitstop += player1Renderer.CheckCollision();
            if (p1Hitstop > 0)
                player1Pinball.GetComponent<Rigidbody2D>().simulated = false;
        }
        else
        {
            p1Hitstop--;
            if(p1Hitstop == 0)
            {
                player1Pinball.GetComponent<Rigidbody2D>().simulated = true;
                player1.Hitstop = false;
            }
        }

        if (p2Hitstop <= 0)
        {
            //player2.FGUpdate();
            //p2Hitstop += player2Renderer.CheckCollision();
        }
        else
            p2Hitstop--;

        player1.FGDraw();
        player1.FGDrawHitboxes();
        //player2.FGDraw();
        //player2.FGDrawHitboxes();

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

    public void Rematch()
    {
        pinballManager.Init();
        Init();
        uiManager.Init();
    }
}
