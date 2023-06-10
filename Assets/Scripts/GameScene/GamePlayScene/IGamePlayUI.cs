using UnityEngine.UI;

namespace GameScene.GamePlayScene
{
    public interface IGamePlayUI
    {
        public void UpdateScore(int score);
        public void UpdateTimeRemaining(int timeRemaining);
        public void UpdateOpportunity(int opportunity);
    }
}