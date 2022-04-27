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
    public static float multiplierP1 { get; set; }
    public static int scoreP2 { get; private set; }
    public static float multiplierP2 { get; set; }
    public static EventType currentEventP1 { get; private set; }
    public static float eventTimerP1 { get; private set; }
    public static EventType currentEventP2 { get; private set; }
    public static float eventTimerP2 { get; private set; }
    public static int bumperHitsP1 { get; set; }
    public static int bumperHitsP2 { get; set; }

    public enum RailType
    {
        curved = 0,
        ramp = 1
    }

    public enum EventType
    {
        NO_EVENT,
        hitBumpers,
        leftRamp
    }

    //Game-Loop Data
    [SerializeField] public FGRenderer player1Renderer, player2Renderer;
    [SerializeField] private GameObject player1Pinball, player2Pinball;
    FGFighter player1, player2;
    int p1Hitstop, p2Hitstop;

    //public void GetPoints()
    //{
    //    scoreP1++;
    //}

    [SerializeField] private GameObject ui;
    private UIManager uiManager;

    private PinballManager pinballManager1;
    private PinballManager pinballManager2;

    /// <summary>
    /// Initialisation method.
    /// </summary>
    private void Init()
    {
        uiManager = ui.GetComponent<UIManager>();
        pinballManager1 = player1Pinball.GetComponent<PinballManager>();
        pinballManager2 = player2Pinball.GetComponent<PinballManager>();

        Time.fixedDeltaTime = 1f / 60.0f; //enforce 60 FPS

        scoreP1 = 0;
        scoreP2 = 0;
        multiplierP1 = 1;
        multiplierP2 = 1;
        bumperHitsP1 = 3;
        bumperHitsP2 = 3;

        //Character initialization
        //Specifically called in Start() not Awake() to wait for any data to get passed in from hypothetical singleton
        player1 = new Hipster(player1Renderer);
        player1Renderer.fighter = player1;
        player1.FGUpdate();
        player1.FGDraw();
        player2 = new Hipster(player2Renderer);
        player2Renderer.fighter = player2;

        player2.facingLeft = true;

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
                Vector2 ballDI = new Vector2(player1.Joystick.x, 0) * 0.2f;

                player1Pinball.GetComponent<Rigidbody2D>().velocity = new Vector2(player1Renderer.hitDetected.velocity.x * (player1.facingLeft ? -1 : 1), player1Renderer.hitDetected.velocity.y) + (ballDI * player1Renderer.hitDetected.velocity.magnitude);
                player1Pinball.GetComponent<Rigidbody2D>().simulated = true;
                player1.Hitstop = false;
            }
        }

        if (p2Hitstop <= 0)
        {
            player2.FGUpdate();
            p2Hitstop += player2Renderer.CheckCollision();
            if (p2Hitstop > 0)
                player2Pinball.GetComponent<Rigidbody2D>().simulated = false;
        }
        else
        {
            p2Hitstop--;
            if (p2Hitstop == 0)
            {
                Vector2 ballDI = new Vector2(player2.Joystick.x, 0) * 0.2f;

                player2Pinball.GetComponent<Rigidbody2D>().velocity = new Vector2(player2Renderer.hitDetected.velocity.x * (player2.facingLeft ? -1 : 1), player2Renderer.hitDetected.velocity.y) + (ballDI * player2Renderer.hitDetected.velocity.magnitude);
                player2Pinball.GetComponent<Rigidbody2D>().simulated = true;
                player2.Hitstop = false;
            }
        }

        player1.FGDraw();
        player1.FGDrawHitboxes();
        player2.FGDraw();
        player2.FGDrawHitboxes();

    }

    public static void issuePoints(int points, int player = 1)
    {
        if (player == 1)
        {
            scoreP1 += (int)(points * multiplierP1);
        }
        if (player == 2)
        {
            scoreP2 += (int)(points * multiplierP2);
        }
    }

    public void Rematch()
    {
        pinballManager1.Init();
        pinballManager2.Init();
        Init();
        uiManager.Init();
    }

    public static void BeginEvent(EventType et, int player = 1)
    {
        if(player == 1) //Create event for player 1
        {
            currentEventP1 = et;
            switch (et)
            {
                case EventType.hitBumpers:
                    eventTimerP1 = 20f;
                    break;
                case EventType.leftRamp:
                    eventTimerP1 = 10f;
                    break;
            }
        }
        else //Create event for player 2
        {
            currentEventP2 = et;
            switch (et)
            {
                case EventType.hitBumpers:
                    eventTimerP2 = 20f;
                    break;
                case EventType.leftRamp:
                    eventTimerP2 = 10f;
                    break;
            }
        }
    }

    public static void EventUpdate(EventType et, int player)
    {
        switch(et)
        {
            case EventType.hitBumpers:
                if (player == 1)
                {
                    eventTimerP1 -= Time.deltaTime;
                }
                else
                {
                    eventTimerP2 -=Time.deltaTime;
                }
                break;
            case EventType.leftRamp:
                if (player == 1)
                {
                    eventTimerP1 -= Time.deltaTime;
                }
                else
                {
                    eventTimerP2 -= Time.deltaTime;
                }
                break;
        }
        if(eventTimerP1 <= 0)
        {
            currentEventP1 = EventType.NO_EVENT;
            //Display event over text here.
        }
        if (eventTimerP2 <= 0)
        {
            currentEventP2 = EventType.NO_EVENT;
            //Display event over text here.
        }
    }

    public static void EventComplete(EventType et, int player)
    {
        switch (et)
        {
            case EventType.hitBumpers:
                if (player == 1)
                {
                    issuePoints(1000, 1);
                    //Visual feedback
                }
                else
                {
                    issuePoints(1000, 2);
                    //Visual feedback
                }
                break;
            case EventType.leftRamp:
                if (player == 1)
                {
                    issuePoints(250, 1);
                    multiplierP1 += .75f;
                    //Visual feedback
                }
                else
                {
                    issuePoints(250, 2);
                    multiplierP2 += .75f;
                    //Visual feedback
                }
                break;
        }
    }
}
