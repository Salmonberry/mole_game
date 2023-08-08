using System;
using UnityEngine;

namespace Domin.Enitiy
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField]
        private int _score = 0;
        [SerializeField]
        private int _opportunity = 3;

        public void UpdateScore(int score)
        {
            _score += score;
        }

        public void UpdateOpportunity(int opportunity)
        {
            _opportunity -= opportunity;
        }

        public int GetScore()
        {
            return _score;
        }

        public int GetOpportunity()
        {
            return _opportunity;
        }
    }
}