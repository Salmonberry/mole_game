using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.GamePlayScene
{
    public class GamePlayUI : MonoBehaviour, IGamePlayUI
    {
        public TextMeshProUGUI Score;
        public TextMeshProUGUI TimeRemaining;
        public TextMeshProUGUI Opportunity;

        public void UpdateScore(int score)
        {
            Score.text = score.ToString();
        }

        public void UpdateTimeRemaining(int timeRemaining)
        {
            TimeRemaining.text = timeRemaining.ToString();
        }

        public void UpdateOpportunity(int opportunity)
        {
            Opportunity.text = opportunity.ToString();
        }
    }
}