using System.Threading.Tasks;
using UnityEngine;

namespace VRJam23
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] p_FlyingEnemies;
        [SerializeField] private GameObject[] p_GroundEnemies;

        private bool pr_FlyingEnemyActive;
        private bool pr_GroundEnemyActive;

        private int pr_FlyingSpawnDelayLower;
        private int pr_FlyingSpawnDelayUpper;
        private int pr_FlyingSpawnDelay;

        private int pr_GroundSpawnDelayLower;
        private int pr_GroundSpawnDelayUpper;
        private int pr_GroundSpawnDelay;

        private int pr_FirstSpawnDelayLower;
        private int pr_FirstSpawnDelayUpper;
        private int pr_FirstSpawnDelay;

        private bool pr_SpawnLoopActive;

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
            Setup();
        }

        private void GameEnded()
        { 
        
        }

        private void Setup()
        {
            pr_FirstSpawnDelay = Random.Range(pr_FirstSpawnDelayLower, pr_FirstSpawnDelayUpper);
            FirstSpawn();
        }

        private async void FirstSpawn()
        {
            await Task.Delay(pr_FirstSpawnDelay);
        }

        private async void SpawnLoop()
        { 
            
        }
    }
}
