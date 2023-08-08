using TMPro;
using UnityEngine;

namespace GameScene.GamePlayScene
{
    public class GamePlayUI : MonoBehaviour, IGamePlayUI
    {
        public TextMeshProUGUI score;
        public TextMeshProUGUI timeRemaining;
        public TextMeshProUGUI opportunity;

        public void UpdateScore(int score)
        {
            this.score.text = $"Score:{score.ToString()}";
        }

        public void UpdateTimeRemaining(int timeRemaining)
        {
            this.timeRemaining.text = $"Time Remaining:{timeRemaining.ToString()}";
        }

        public void UpdateOpportunity(int opportunity)
        {
            this.opportunity.text = $"Opportunity:{opportunity.ToString()}";
        }
    }
}