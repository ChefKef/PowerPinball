using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Menu button action methods that trigger a change in game state.
/// </summary>
public class Menu : MonoBehaviour
{
    public void ToMainMenu()
    {
        StateManager.Instance.SetState(GameStates.MainMenu);
    }

    public void ToGame()
    {
        StateManager.Instance.SetState(GameStates.Game);
    }

    public void ToWin()
    {
        StateManager.Instance.SetState(GameStates.Win);
    }

    public void ToLose()
    {
        StateManager.Instance.SetState(GameStates.Lose);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
