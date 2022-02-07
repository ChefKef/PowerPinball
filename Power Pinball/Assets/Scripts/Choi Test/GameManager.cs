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
    
    public void GetPoints()
    {
        Score++;
    }

    public static void Reset()
    {
        Score = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.fixedDeltaTime = 1f / 60.0f; //enforce 60 FPS

        Score = 0;
    }

    //Updates at a fixed rate, as opposed to Update() which is reliant on the rendering pipeline.
    //Set to 60FPS in Start()
    void FixedUpdate() 
    {

    }
}
