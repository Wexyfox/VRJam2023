using TMPro;
using UnityEngine;

namespace VRJam23
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private Difficulty s_Difficulty;
        [SerializeField] private int pr_CurrentScore;
        [SerializeField] private TextMeshProUGUI u_ScoreLabel;

        private void OnEnable()
        {
            GameEvents.ScoreChange += ScoreChange;
        }

        private void OnDisable()
        {
            GameEvents.ScoreChange -= ScoreChange;
        }

        private void Start()
        {
            pr_CurrentScore = 0;
        }

        private void ScoreChange(int pa_ScoreChange)
        {
            pr_CurrentScore += pa_ScoreChange;
            u_ScoreLabel.text = pr_CurrentScore.ToString();
            s_Difficulty.DifficultyIncreaseCheck(pa_ScoreChange);
        }
    }
}
