using UnityEngine;

namespace VRJam23
{
    public class Difficulty : MonoBehaviour
    {
        [SerializeField] private ScoreCounter s_ScoreCounter;
        [SerializeField] private float pr_DifficultyMultiplier;
        [SerializeField] private float pr_DifficultyIncreaseDifference;
        [SerializeField] private int pr_DifficultyIncreaseThreshold;

        private int pr_RollingModulusScore;

        private void Start()
        {
            pr_RollingModulusScore = 0;
            pr_DifficultyMultiplier = 1f;
        }

        public void DifficultyIncreaseCheck(int pa_NewScoreAdded)
        {
            pr_RollingModulusScore += pa_NewScoreAdded;

            if (pr_RollingModulusScore > pr_DifficultyIncreaseThreshold)
            {
                pr_DifficultyMultiplier += pr_DifficultyIncreaseDifference;
                pr_RollingModulusScore = pr_RollingModulusScore % pr_DifficultyIncreaseThreshold;

                GameEvents.InvokeDifficultyScalarChange(pr_DifficultyMultiplier);
            }
        }

        public float CurrentDifficulty()
        {
            return pr_DifficultyMultiplier;
        }
    }
}
