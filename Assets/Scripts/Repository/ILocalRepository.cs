namespace Repository
{
    public interface ILocalRepository
    {
        public void SaveScore(float data);
        public float GetScore();
        
        public  void SaveTimeRemaining(float data);
        public float GetTimeRemaining();

        public void SaveOpportunity(float data);
        public float GetOpportunity();

        public void ClearAllData();

    }
}