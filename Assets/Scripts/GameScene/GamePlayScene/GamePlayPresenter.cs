using System;
using System.Collections;
using System.Linq;
using CustomArrayExtensions;
using Domin.Enitiy;
using Domin.Event;
using GameScene.GamePlayScene;
using Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayPresenter : MonoBehaviour
{
    public GameObject gopher;
    private Transform[] _gopherList;
    private Vector3 _previousHolePosition;

    [Header("VFX")] public GameObject explosion;

    [Header("Sound Configure")] 
    public AudioClip gameStartReadyVoiceEffect;
    public AudioSource audioSource;

    [Header("Game UI")] public GameObject gameEndPanel;
    public GamePlayUI gamePlayUI;

    [Header("Time CuteDown")] [SerializeField]
    public float timeRemaining = 60f;

    [Header("Player Data")] [SerializeField]
    public PlayerData playerData;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        InitData();
        
        var playButton = GetChildByName(gamePlayUI.gameObject, "Button_Play");
        
        playButton.GetComponent<Button>().onClick.AddListener((() =>
        {
            var gameStartPanel = playButton.transform.parent.gameObject;
            gameStartPanel.SetActive(false);
            
            GameSystem.Instance.UpdateGameState(GameState.GamePlaying);
            InvokeRepeating(nameof(GenerateGopher), 0, 3);
        }));
    }

    void InitData()
    {
        _gopherList = GenerateHoles();
        playerData= GetGameModelData();
        
        gamePlayUI.UpdateScore(playerData.GetScore());
        gamePlayUI.UpdateTimeRemaining(60);
        gamePlayUI.UpdateOpportunity(playerData.GetOpportunity());
        
        ScoreCalculationEvent.Register(UpdateScore);
        GameStartReadyEvent.Register(ShowGameReadyMusic);
        GameEndEvent.Register(ShowGameEndPanel);
        GameOpportunityEvent.Register(UpdateOpportunity);
        GamePlayingEvent.Register(()=>StartCoroutine(CountDownTimeRemaining()));
    }

    private PlayerData GetGameModelData()
    {
        var gameModelManager = GameObject.FindObjectOfType<GameModelManager>();
        return gameModelManager.LoadData("PlayerData");
    }

    private void OnDestroy()
    {
        ScoreCalculationEvent.Unregister(UpdateScore);
        GameStartReadyEvent.Unregister(ShowGameReadyMusic);
        GameEndEvent.Unregister(ShowGameEndPanel);
        GamePlayingEvent.Unregister(()=>StartCoroutine(CountDownTimeRemaining()));
    }

    private GameObject GetChildByName(GameObject parent, string childName)
    {
        var children = parent.transform.parent.GetComponentsInChildren<Transform>();

        return (from child in children where child.name == childName select child.gameObject).FirstOrDefault();
    }

    void UpdateScore(int data)
    {
        playerData.UpdateScore(data);
        gamePlayUI.UpdateScore(playerData.GetScore());
    }

    void UpdateOpportunity()
    {
        playerData.UpdateOpportunity(1);
        gamePlayUI.UpdateOpportunity(playerData.GetOpportunity());
    }

    private IEnumerator CountDownTimeRemaining()
    {
        while (timeRemaining>0)
        {
            yield return new WaitForSeconds(1);
            timeRemaining--;
            
            gamePlayUI.UpdateTimeRemaining((int) timeRemaining);
        }
        
        timeRemaining = 0;
        GameSystem.Instance.UpdateGameState(GameState.GameOver);
        CancelInvoke(); 
    }

    public void GenerateGopher()
    {
        Vector3 randomPosition = GetRandomPosition(_gopherList);
        randomPosition.y += 0.5f;

        Instantiate(gopher, randomPosition, Quaternion.identity);
    }

    private Transform[] GenerateHoles()
    {
        var holes = new Transform[] { };

        var parent = GameObject.Find("Holes");

        return parent == null
            ? holes
            : parent.transform.Cast<Transform>().Aggregate(holes, (current, child) => current.Append(child).ToArray());
    }

    private Vector3 GetRandomPosition(Transform[] data)
    {
        var currentPosition = data.GetRandom().position;

        if (currentPosition == _previousHolePosition)
        {
            currentPosition = data.GetRandom().position;
        }

        _previousHolePosition = currentPosition;


        return currentPosition;
    }

    private void ShowGameReadyMusic()
    {
        audioSource.clip = gameStartReadyVoiceEffect;
        audioSource.loop = true;
        audioSource.Play();

        var vfx = new GameObject
        {
            name = "VFX"
        };

        var rainVFX = Instantiate(explosion, explosion.transform.position, Quaternion.identity);
        rainVFX.GetComponent<ParticleSystem>().Play();

        rainVFX.transform.parent = vfx.transform;
    }

    private void ShowGameEndPanel()
    {
        gameEndPanel.SetActive(true);
        var exitButton = GetChildByName(gameEndPanel, "Button_Exit");
        var restartButton = GetChildByName(gameEndPanel, "Button_Restart");

        print(exitButton);
        print(restartButton);

        exitButton.GetComponent<Button>().onClick.AddListener(ExitGame);
        restartButton.GetComponent<Button>().onClick.AddListener(RestartGame);
    }


    // exit the game
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}