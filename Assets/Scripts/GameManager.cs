using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Variables")]
    public static GameManager instance;
    public GameState state;
    public static event Action<GameState> OnGameStateChanged;

    private void Awake() 
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            UpdateGameState(GameState.Menu);
        else if (SceneManager.GetActiveScene().buildIndex == 1)
            UpdateGameState(GameState.Game);
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;
        switch (newState)
        {
            case GameState.Menu:
                break;
            case GameState.Game:
                break;
            case GameState.Win:
                UIManager.instance.Victory();
                break;
            case GameState.GameOver:
                UIManager.instance.GameOver();
                break;
            case GameState.Paused:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }
}

public enum GameState
{
    Menu,
    Game,
    Win,
    GameOver,
    Paused
}
