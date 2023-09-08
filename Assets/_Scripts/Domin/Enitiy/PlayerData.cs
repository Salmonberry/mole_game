using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Domin.Enitiy
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private int score = 0;
        [SerializeField] private int opportunity = 3;

        public void UpdateScore(int score)
        {
            this.score += score;
        }

        public void UpdateOpportunity(int opportunity)
        {
            this.opportunity -= opportunity;
        }

        public void ReSetOpportunity()
        {
            opportunity = 3;
        }

        public void ReSetScore()
        {
            score = 0;
        }

        public int GetScore()
        {
            return score;
        }

        public int GetOpportunity()
        {
            return opportunity;
        }
    }
}