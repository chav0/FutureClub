using UnityEngine;

namespace Project.Scripts.Models
{
    public class ApplicationModel : IApplicationModel
    {
        public int BestScore { get; private set; }
        public int Coins { get; private set; }
        public float LastLevelProgress { get; private set; }
        public int CurrentLevelReached { get; private set; }

        public ApplicationModel()
        {
            BestScore = PlayerPrefs.GetInt("Score", 0);
            Coins = PlayerPrefs.GetInt("Coins", 0);
            LastLevelProgress = PlayerPrefs.GetFloat("Progress", 0f); 
            CurrentLevelReached = PlayerPrefs.GetInt("Level", 0);
        }
        
        public void Update()
        {
            
        }

        public void ReportGameOverWithScore(int score, int coinsCollected, float progress)
        {
            if (BestScore < score)
                BestScore = score;

            Coins += coinsCollected;
            
            PlayerPrefs.SetInt("Score", BestScore);
            PlayerPrefs.SetInt("Coins", Coins);
            PlayerPrefs.SetFloat("Progress", progress);
        }

        public void ReportLevelPassWithCScore(int level, int score, int coinsCollected)
        {
            if (BestScore < score)
                BestScore = score;

            CurrentLevelReached = level;

            Coins += coinsCollected; 
            
            PlayerPrefs.SetInt("Score", BestScore);
            PlayerPrefs.SetInt("Coins", Coins);
            PlayerPrefs.SetInt("Level", 0);
        }
    }
}
