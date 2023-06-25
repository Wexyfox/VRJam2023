using UnityEngine;

namespace VRJam23
{
    public class GameClose : MonoBehaviour
    {
        private void OnApplicationQuit()
        {
            GameEvents.InvokeGameEnded();
        }
    }
}
