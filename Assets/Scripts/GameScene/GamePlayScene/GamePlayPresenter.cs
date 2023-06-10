using System.Linq;
using CustomArrayExtensions;
using GameScene.GamePlayScene;
using UnityEngine;

public class GamePlayPresenter : MonoBehaviour
{
    public GameObject gopher;
    private Transform[] _gopherList;
    private Vector3 _previousHolePosition;

    public GamePlayUI gamePlayUI;

    [Header("Time CuteDown")] [SerializeField]
    public float timeRemaining = 10;

    [SerializeField] private bool _timerIsRunning = false;


    void Start()
    {
        _gopherList = GenerateHoles();

        InvokeRepeating(nameof(GenerateGopher), 0, 3);
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

    // Unity Game time countdown
}