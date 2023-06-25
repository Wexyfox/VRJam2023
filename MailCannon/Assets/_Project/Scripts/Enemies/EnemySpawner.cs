using System.Threading.Tasks;
using UnityEngine;

namespace VRJam23
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemySetup s_EnemySetup;

        [Header("Enemies")]
        [SerializeField] private GameObject[] p_FlyingEnemies;
        [SerializeField] private GameObject[] p_GroundEnemies;

        [Header("Spawn Positions")]
        [SerializeField] private Transform u_FlyingLeftSpawnPosition;
        [SerializeField] private Transform u_FlyingRightSpawnPosition;
        [SerializeField] private Transform u_GroundLeftSpawnPosition;
        [SerializeField] private Transform u_GroundRightSpawnPosition;
        [SerializeField] private Transform u_StorageInstantiationPosition;

        private float pr_DifficultyScalar = 1f;

        private int pr_FlyingSpawnDelayLower = 20000;
        private int pr_FlyingSpawnDelayUpper = 30000;
        private int pr_FlyingSpawnDelay;

        private int pr_GroundSpawnDelayLower = 15000;
        private int pr_GroundSpawnDelayUpper = 25000;
        private int pr_GroundSpawnDelay;

        private int pr_FirstSpawnDelayLower = 30000;
        private int pr_FirstSpawnDelayUpper = 40000;
        private int pr_FirstSpawnDelay;

        private bool pr_FirstSpawnedEnemyDefeated;
        private bool pr_SpawnActive;

        private GameObject p_NewSpawnedEnemy;
        private Enemy s_NewEnemy;
        private EnemyTypeEnum s_NewEnemyEnum;
        private Transform u_NewEnemyTransform;
        private bool pr_NewEnemyLeftSideSpawn;

        private void OnEnable()
        {
            GameEvents.GameStart += GameStart;
            GameEvents.GameEnded += GameEnded;
            GameEvents.DifficultyScalarChange += DifficultyScalarChange;
        }

        private void OnDisable()
        {
            GameEvents.GameStart -= GameStart;
            GameEvents.GameEnded -= GameEnded;
            GameEvents.DifficultyScalarChange -= DifficultyScalarChange;
        }

        private void GameStart()
        {
            Setup();
        }

        private void GameEnded()
        {
            pr_SpawnActive = false;
        }

        private void DifficultyScalarChange(float pa_NewDifficultyScalar)
        {
            pr_DifficultyScalar = pa_NewDifficultyScalar;
        }

        private void Setup()
        {
            pr_FirstSpawnDelay = Random.Range(pr_FirstSpawnDelayLower, pr_FirstSpawnDelayUpper);
            pr_FirstSpawnedEnemyDefeated = false;
            FirstSpawn();
        }

        private async void FirstSpawn()
        {
            await Task.Delay(pr_FirstSpawnDelay);
            p_NewSpawnedEnemy = Instantiate(
                p_GroundEnemies[0],
                u_StorageInstantiationPosition.position,
                u_StorageInstantiationPosition.rotation);

            s_NewEnemy = p_NewSpawnedEnemy.GetComponent<Enemy>();
            s_NewEnemyEnum = s_NewEnemy.Type();

            u_NewEnemyTransform = p_NewSpawnedEnemy.transform;
            pr_NewEnemyLeftSideSpawn = Random.value > 0.5f;

            ChooseSpawnPosition(u_NewEnemyTransform, s_NewEnemyEnum, pr_NewEnemyLeftSideSpawn);
            s_EnemySetup.Setup(p_NewSpawnedEnemy, this, pr_NewEnemyLeftSideSpawn, pr_DifficultyScalar);
        }

        private async void FlyingSpawn()
        {
            if (!pr_SpawnActive) return;
            int l_UnmodifiedDelay = Random.Range(pr_FlyingSpawnDelayLower, pr_FlyingSpawnDelayUpper);
            pr_FlyingSpawnDelay = Mathf.RoundToInt(l_UnmodifiedDelay / pr_DifficultyScalar);

            await Task.Delay(pr_FlyingSpawnDelay);

            int l_RandomIndex = Random.Range(0, p_FlyingEnemies.Length);
            p_NewSpawnedEnemy = Instantiate(
                p_FlyingEnemies[l_RandomIndex],
                u_StorageInstantiationPosition.position,
                u_StorageInstantiationPosition.rotation);

            s_NewEnemy = p_NewSpawnedEnemy.GetComponent<Enemy>();
            s_NewEnemyEnum = s_NewEnemy.Type();

            u_NewEnemyTransform = p_NewSpawnedEnemy.transform;
            pr_NewEnemyLeftSideSpawn = Random.value > 0.5f;

            ChooseSpawnPosition(u_NewEnemyTransform, s_NewEnemyEnum, pr_NewEnemyLeftSideSpawn);
            s_EnemySetup.Setup(p_NewSpawnedEnemy, this, pr_NewEnemyLeftSideSpawn, pr_DifficultyScalar);
        }

        private async void GroundSpawn()
        {
            if (!pr_SpawnActive) return;
            int l_UnmodifiedDelay = Random.Range(pr_GroundSpawnDelayLower, pr_GroundSpawnDelayUpper);
            pr_GroundSpawnDelay = Mathf.RoundToInt(l_UnmodifiedDelay / pr_DifficultyScalar);

            await Task.Delay(pr_GroundSpawnDelay);

            int l_RandomIndex = Random.Range(0, p_GroundEnemies.Length);
            p_NewSpawnedEnemy = Instantiate(
                p_GroundEnemies[l_RandomIndex],
                u_StorageInstantiationPosition.position,
                u_StorageInstantiationPosition.rotation);

            s_NewEnemy = p_NewSpawnedEnemy.GetComponent<Enemy>();
            s_NewEnemyEnum = s_NewEnemy.Type();

            u_NewEnemyTransform = p_NewSpawnedEnemy.transform;
            pr_NewEnemyLeftSideSpawn = Random.value > 0.5f;

            ChooseSpawnPosition(u_NewEnemyTransform, s_NewEnemyEnum, pr_NewEnemyLeftSideSpawn);
            s_EnemySetup.Setup(p_NewSpawnedEnemy, this, pr_NewEnemyLeftSideSpawn, pr_DifficultyScalar);
        }

        private void ChooseSpawnPosition(Transform pa_NewEnemyTransform, EnemyTypeEnum pa_EnemyType, bool pa_LeftSpawn)
        {
            switch (pa_EnemyType)
            {
                case EnemyTypeEnum.FLYING:
                    if (pa_LeftSpawn)
                    {
                        u_NewEnemyTransform.SetPositionAndRotation(
                            u_FlyingLeftSpawnPosition.position,
                            u_FlyingLeftSpawnPosition.rotation);
                    }
                    else
                    {
                        u_NewEnemyTransform.SetPositionAndRotation(
                            u_FlyingRightSpawnPosition.position,
                            u_FlyingRightSpawnPosition.rotation);
                    }
                    break;
                case EnemyTypeEnum.GROUND:
                    if (pa_LeftSpawn)
                    {
                        u_NewEnemyTransform.SetPositionAndRotation(
                            u_GroundLeftSpawnPosition.position,
                            u_GroundLeftSpawnPosition.rotation);
                    }
                    else
                    {
                        u_NewEnemyTransform.SetPositionAndRotation(
                            u_GroundRightSpawnPosition.position,
                            u_GroundRightSpawnPosition.rotation);
                    }
                    break;
            }
        }

        public void FlyingEnemyDefeated()
        {
            FlyingSpawn();
        }

        public void GroundEnemyDefeated()
        {
            GroundSpawn();
            if (!pr_FirstSpawnedEnemyDefeated)
            {
                pr_FirstSpawnedEnemyDefeated = true;
                pr_SpawnActive = true;
                FlyingSpawn();
                GroundSpawn();
            }
        }

        public void SetDifficulty(float pa_NewDifficultyScalar)
        {
            pr_DifficultyScalar = pa_NewDifficultyScalar;
        }
    }
}
