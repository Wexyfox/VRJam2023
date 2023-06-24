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

        public static event UnityAction<float> SpeedChange;
        public static void InvokeSpeedChange(float pa_NewSpeed)
        {
            SpeedChange?.Invoke(pa_NewSpeed);
        }

        public static event UnityAction<int> ScoreChange;
        public static void InvokeScoreChange(int pa_ScoreChangeAmount)
        {
            ScoreChange?.Invoke(pa_ScoreChangeAmount);
        }
    }
}
