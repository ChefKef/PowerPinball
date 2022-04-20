using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Menu button action methods that trigger a change in game state.
/// </summary>
public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject instructionsPopUp;

    private void Start()
    {
        HideInstructions();
    }

    public void ToMainMenu()
    {
        StateManager.Instance.SetState(GameStates.MainMenu);
    }

    public void ShowInstructions()
    {
        //StateManager.Instance.SetState(GameStates.Instructions);
        instructionsPopUp.SetActive(true);
    }

    public void HideInstructions()
    {
        //StateManager.Instance.SetState(GameStates.Instructions);
        instructionsPopUp.SetActive(false);
        //sd 
    }

    public void ToCharacterCustomisation()
    {
        StateManager.Instance.SetState(GameStates.CharacterCustomisation);
    }

    public void ToGame()
    {
        //GameManager.Init();
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

    public void Quit()
    {
        Application.Quit();
    }
}
