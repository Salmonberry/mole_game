using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    private static GameUIManager instance;

    public Text Score;
    public Text TimeRemaining;
    public Text Opportunity;

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

    public static GameUIManager Instance
    {
        get { return instance; }
    }


    public void UpdateScore(int score)
    {
            Score.text = score.ToString();
    }

    public void UpdateTimeRemaining(float time)
    {
        TimeRemaining.text = time.ToString();
    }

    public void UpdateOpportunity(int opportunity)
    {
        Opportunity.text = opportunity.ToString();

    }
}
