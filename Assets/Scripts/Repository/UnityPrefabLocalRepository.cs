using UnityEngine;

namespace Repository
{
    public class UnityPrefabLocalRepository : ILocalRepository
    {
        public void SaveScore(float data)
        {
            PlayerPrefs.SetFloat("Score", data);
        }

        public float GetScore()
        {
            return PlayerPrefs.GetFloat("Score");
        }

        public void SaveTimeRemaining(float data)
        {
            PlayerPrefs.SetFloat("TimeRemaining", data);
        }

        public float GetTimeRemaining()
        {
            return PlayerPrefs.GetFloat("TimeRemaining");
        }

        public void SaveOpportunity(float data)
        {
            PlayerPrefs.SetFloat("Opportunity", data);
        }

        public float GetOpportunity()
        {
            return PlayerPrefs.GetFloat("Opportunity");
        }

        public void ClearAllData()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}