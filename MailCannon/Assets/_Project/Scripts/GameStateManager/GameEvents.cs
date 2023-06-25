using UnityEngine.Events;

namespace VRJam23
{
    public static class GameEvents
    {
        public static event UnityAction GameStart;
        public static void InvokeGameStart()
        {
            GameStart?.Invoke();
        }

        public static event UnityAction GameEnded;
        public static void InvokeGameEnded()
        {
            GameEnded?.Invoke();
        }

        public static event UnityAction<float> DifficultyScalarChange;
        public static void InvokeDifficultyScalarChange(float pa_NewDifficultyScalar)
        {
            DifficultyScalarChange?.Invoke(pa_NewDifficultyScalar);
        }

        public static event UnityAction<int> ScoreChange;
        public static void InvokeScoreChange(int pa_ScoreChangeAmount)
        {
            ScoreChange?.Invoke(pa_ScoreChangeAmount);
        }

        public static event UnityAction Pothole;
        public static void InvokePothole()
        {
            Pothole?.Invoke();
        }
    }
}
