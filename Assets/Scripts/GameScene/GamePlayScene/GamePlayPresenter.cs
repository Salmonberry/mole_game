using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CustomArrayExtensions;
using Domin.Enitiy;
using Domin.Event;
using Model;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameScene.GamePlayScene
{
    public class GamePlayPresenter : MonoBehaviour
    {
        [Header("VFX")] public GameObject explosion;
        [Header("Sound Configure")] public AudioClip gameStartReadyVoiceEffect;
        public AudioSource audioSource;
        [Header("Game Tips Panel")] public GameObject gameTipsPanel;
        [Header("Game End Panel")] public GameObject gameEndPanel;
        [Header("Time CuteDown")] public float timeRemaining = 60f;
        [Header("Player Data")] public PlayerData playerData;
        [Header("Game Play UI")] public GamePlayUI gamePlayUI;
        [Header("Game Model Manager")] private GameModelManager gameModelManager;

        public GameObject gopherPrefab;
        private Transform[] _gopherList;
        private Vector3 _previousHolePosition;
        private GameObject _gopher;
        private float _timer;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        void Start()
        {
            InitData();
            BindUI();
        }

        void InitData()
        {
            try
            {
                _gopherList = GenerateHoles();
                playerData = GetGameModelData();
                _timer = timeRemaining;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void BindUI()
        {
            gamePlayUI.UpdateScore(playerData.GetScore());
            gamePlayUI.UpdateTimeRemaining((int) _timer);
            gamePlayUI.UpdateOpportunity(playerData.GetOpportunity());

            ScoreCalculationEvent.Register(UpdateScore);
            GameStartReadyEvent.Register(ShowGameReadyMusic);
            // GameEndEvent.Register();
            GameOpportunityEvent.Register(UpdateOpportunity);
            GamePauseEvent.Register(ShowGameTipsPanel);
            GamePlayingEvent.Register(() => StartCoroutine(CountDownTimeRemaining()));

            var playButton = GetChildByName(gamePlayUI.gameObject, "Button_Play");

            playButton.GetComponent<Button>().onClick.AddListener((() =>
            {
                var gameStartPanel = playButton.transform.parent.gameObject;
                gameStartPanel.SetActive(false);

                GameSystem.Instance.UpdateGameState(GameState.GamePlaying);
                InvokeRepeating(nameof(GenerateGopher), 0, 0.5f);
            }));
        }

        private PlayerData GetGameModelData()
        {
            gameModelManager = FindObjectOfType<GameModelManager>();

            return gameModelManager.LoadData("PlayerData");
        }

        private void OnDestroy()
        {
            ScoreCalculationEvent.Unregister(UpdateScore);
            GameStartReadyEvent.Unregister(ShowGameReadyMusic);
            GameEndEvent.Unregister(ShowGameTipsPanel);
            GamePlayingEvent.Unregister(() => StartCoroutine(CountDownTimeRemaining()));
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
            while (_timer > 0)
            {
                yield return new WaitForSeconds(1);
                --_timer;

                gamePlayUI.UpdateTimeRemaining((int) _timer);
            }

            CancelInvoke();

            if (playerData.GetOpportunity() <= 0)
            {
                ShowGameEndPanel();
            }
            else
            {
                GameSystem.Instance.UpdateGameState(GameState.GamePause);
            }

            _timer = 0;

            if (_gopher != null)
            {
                _gopher.GetComponent<Gopher>().UnenabledTapped();
            }
        }

        public void GenerateGopher()
        {
            Vector3 randomPosition = GetRandomPosition(_gopherList);
            randomPosition.y += 0.5f;

            if (_gopher == null)
            {
                _gopher = Instantiate(gopherPrefab, randomPosition, Quaternion.identity);
            }
        }

        private Transform[] GenerateHoles()
        {
            var holes = new Transform[] { };

            var parent = GameObject.Find("Holes");

            return parent == null
                ? holes
                : parent.transform.Cast<Transform>()
                    .Aggregate(holes, (current, child) => current.Append(child).ToArray());
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

        private void ShowGameTipsPanel()
        {
            gameTipsPanel.SetActive(true);

            var exitButton = GetChildByName(gameTipsPanel, "Button_Exit");
            var continueButton = GetChildByName(gameTipsPanel, "Button_Continue");


            exitButton.GetComponent<Button>().onClick.AddListener(ExitGame);
            continueButton.GetComponent<Button>().onClick.AddListener(ContinueGame);
        }

        private void ShowGameEndPanel()
        {
            gameEndPanel.SetActive(true);

            var exitButton = GetChildByName(gameTipsPanel, "Button_Exit");

            exitButton.GetComponent<Button>().onClick.AddListener(ExitGame);
        }

        private void HideGameEndPanel()
        {
            var exitButton = GetChildByName(gameTipsPanel, "Button_Exit");
            var continueButton = GetChildByName(gameTipsPanel, "Button_Continue");

            exitButton.GetComponent<Button>().onClick.RemoveListener(ExitGame);
            continueButton.GetComponent<Button>().onClick.RemoveListener(ContinueGame);

            gameTipsPanel.SetActive(false);
        }


        // exit the game
        private void ExitGame()
        {
            SaveData();
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void ContinueGame()
        {
            GameOpportunityEvent.Trigger();

            SaveData();

            HideGameEndPanel();

            _timer = timeRemaining;
            StopAllCoroutines();
            GameSystem.Instance.UpdateGameState(GameState.GamePlaying);

            InvokeRepeating(nameof(GenerateGopher), 0, 3);

            if (_gopher != null)
            {
                _gopher.GetComponent<Gopher>().enabledTapped();
            }
        }

        private void SaveData()
        {
            Dictionary<string, PlayerData> dictionary = new Dictionary<string, PlayerData> {{"PlayerData", playerData}};
            gameModelManager.SaveData(dictionary);
        }
    }
}