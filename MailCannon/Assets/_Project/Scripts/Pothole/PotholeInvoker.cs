using System.Threading.Tasks;
using UnityEngine;

namespace VRJam23
{
    public class PotholeInvoker : MonoBehaviour
    {
        [SerializeField] private int pr_PotholeDelayLower = 15000;
        [SerializeField] private int pr_PotholeDelayUpper = 30000;
        [SerializeField] private int pr_PotholeDelay;

        private bool pr_PotholeLoop = false;

        private void OnEnable()
        {
            GameEvents.GameStart += GameStart;
            GameEvents.GameEnded += GameEnded;
        }

        private void OnDisable()
        {
            GameEvents.GameStart -= GameStart;
            GameEvents.GameEnded -= GameEnded;
        }

        private void GameStart()
        {
            pr_PotholeLoop = true;
            PotholeLoop();
        }

        private void GameEnded()
        {
            pr_PotholeLoop = false;
        }

        private async void PotholeLoop()
        {
            while (pr_PotholeLoop)
            {
                pr_PotholeDelay = Random.Range(pr_PotholeDelayLower, pr_PotholeDelayUpper);
                GameEvents.InvokePothole();

                await Task.Delay(pr_PotholeDelay);
            }
        }
    }
}
