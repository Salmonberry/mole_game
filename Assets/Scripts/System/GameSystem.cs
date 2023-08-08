using System;
using Domin.Event;
using UnityEngine;

public enum GameState
{
    GameReady,
    GamePlaying,
    GamePause,
    GameOver
}

public class GameSystem : MonoBehaviour
{
    private static GameSystem instance;
    public GameState GameState { get; private set; }

    public static GameSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameSystem>();
                if (instance == null)
                {
                    GameObject gameObject = new GameObject("GameManager");
                    instance = gameObject.AddComponent<GameSystem>();
                    DontDestroyOnLoad(gameObject);
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        UpdateGameState(GameState.GameReady);
    }
    
    
    

    public void UpdateGameState(GameState state)
    {
        GameState = state;

        switch (state)
        {
            case GameState.GameReady:
                break;
            case GameState.GamePlaying:
                GamePlayingEvent.Trigger();
                break;
            case GameState.GamePause:
                break;
            case GameState.GameOver:
                GameEndEvent.Trigger();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
}