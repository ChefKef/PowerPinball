// Taken and adapted from https://github.com/bttfgames/SimpleGameManager and
// https://www.unitygeek.com/unity_c_singleton/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// All possible states the game can be in.
/// </summary>
public enum GameStates
{
    Title,
    MainMenu,
    Instructions,
    CharacterCustomisation,
    Game,
    Win,
    Lose
}

/// <summary>
/// Handles all zero-argument, void-returning callbacks fired upon state change.
/// </summary>
public delegate void OnStateChangeHandler();

/// <summary>
/// Singleton StateManager implementation for data persistence throughout 
/// program runtime.
/// </summary>
public class StateManager : MonoBehaviour
{
    /// <summary>
    /// Event that gets invoked when game state is altered.
    /// </summary>
    // NOTE: The Awake lifecycle hook is vital to this implementation, and for
    // some reason Unity event subscriptions misbehave during this stage. To
    // that end, C# events are used instead.
    public event OnStateChangeHandler OnStateChange;

    /// <summary>
    /// The current game state.
    /// </summary>
    public GameStates GameState { get; private set; }

    private static StateManager instance;

    #region Singleton Necessities
    public static StateManager Instance
    {
        get
        {
            if (!instance)
            {
                // Search for a component of type T in the Scene.
                instance = FindObjectOfType<StateManager>();

                // If FindObjectOfType() returns null, then the component does
                // not exist in the scene yet. Create an empty GameObject and
                // attach the component to it, since all executable code must
                // be attached to an active GameObject within the Hierarchy.
                //
                // This way, we "lazily" instantiate the singleton (i.e. only
                // create it when it is needed. Therefore, the component, and
                // therefore the GameObject it needs to be attached to, do not
                // need to exist in the Scene beforehand.
                if (!instance)
                {
                    GameObject gObj = new GameObject();
                    gObj.name = typeof(StateManager).Name;
                    instance = gObj.AddComponent<StateManager>();
                }
            }

            return instance;
        }
    }
    #endregion

    #region FSM
    /// <summary>
    /// Changes the game's state and fires all necessary callbacks.
    /// </summary>
    /// <param name="newState"></param>
    public void SetState(GameStates newState)
    {
        // TODO: load new scenes here.
        GameState = newState;
        //OnStateChange.Invoke();

        switch (newState)
        {
            case GameStates.Title:
                SceneManager.LoadSceneAsync("Title");
                break;
            case GameStates.MainMenu:
                SceneManager.LoadSceneAsync("Main Menu");
                break;
            case GameStates.Instructions:
                SceneManager.LoadSceneAsync("Instructions");
                break;
            case GameStates.CharacterCustomisation:
                SceneManager.LoadSceneAsync("Character Customisation");
                break;
            case GameStates.Game:
                SceneManager.LoadSceneAsync("FourthDemoScene"/*"Temp"*/);
                break;
            case GameStates.Win:
                SceneManager.LoadSceneAsync("Win");
                break;
            case GameStates.Lose:
                SceneManager.LoadSceneAsync("Lose");
                break;
        }
    }
    #endregion

    #region Lifecycle Hooks
    public virtual void Awake()
    {
        if (!instance)
        {
            instance = this as StateManager;

            // Because the instance must persist throughout the application,
            // it cannot be destroyed in between scenes.
            DontDestroyOnLoad(this.gameObject);
        }
        // Destroy any additional copies of the StateManager in the Scene.
        else Destroy(this.gameObject);
    }

    protected void OnApplicationQuit()
    {
        // Make instance point to null before the application is quit so the
        // memory allocated for it can be garbage collected.
        instance = null;
    }
    #endregion
}
