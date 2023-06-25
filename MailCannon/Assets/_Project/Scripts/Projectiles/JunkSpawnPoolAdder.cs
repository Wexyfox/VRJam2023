using System.Threading.Tasks;
using UnityEngine;

namespace VRJam23
{
    public class JunkSpawnPoolAdder : MonoBehaviour
    {
        [SerializeField] private ProjectileSpawner s_ProjectileSpawner;
        [SerializeField] private GameObject[] p_JunkPrefabs;

        private int pr_JunkAddDelayLower = 4500;
        private int pr_JunkAddDelayUpper = 6000;
        private int pr_JunkAddDelay;

        private int pr_RandomInt;

        private bool pr_AddJunk = false;

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
            pr_AddJunk = true;
            AddJunkLoop();
        }

        private void GameEnded()
        {
            pr_AddJunk = false;
        }

        private async void AddJunkLoop()
        {
            while (pr_AddJunk)
            {
                pr_JunkAddDelay = Random.Range(pr_JunkAddDelayLower, pr_JunkAddDelayUpper);
                await Task.Delay(pr_JunkAddDelay);

                pr_RandomInt = Random.Range(0, p_JunkPrefabs.Length);
                s_ProjectileSpawner.AddPrefabToSpawnStack(p_JunkPrefabs[pr_RandomInt]);
            }
        }
    }
}
