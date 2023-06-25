using UnityEngine;

namespace VRJam23
{
    public class Caught : MonoBehaviour
    {
        private void OnTriggerEnter(Collider pa_Other)
        {
            if (!pa_Other.GetComponent<Enemy>()) return;

            GameEvents.InvokeGameEnded();
        }
    }
}
