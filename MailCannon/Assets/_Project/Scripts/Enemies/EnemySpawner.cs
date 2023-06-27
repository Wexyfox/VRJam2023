using System.Threading.Tasks;
using UnityEngine;

namespace VRJam23
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemySetup s_EnemySetup;
        [SerializeField] private QuipAudio s_QuipAudio;

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
        private int pr_FlyingSpawnDelayUpper = 35000;
        private int pr_FlyingSpawnDelay;

        private int pr_GroundSpawnDelayLower = 15000;
        private int pr_GroundSpawnDelayUpper = 30000;
        private int pr_GroundSpawnDelay;

        private int pr_FirstSpawnDelayLower = 25000;
        private int pr_FirstSpawnDelayUpper = 45000;
        private int pr_FirstSpawnDelay;

        private bool pr_SpawnActive;

        private GameObject p_NewSpawnedEnemy;
        private Enemy s_NewEnemy;
        private EnemyElevationEnum s_NewEnemyElevationEnum;
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
            
            FirstSpawn();
        }

        private async void FirstSpawn()
        {
            pr_SpawnActive = true;

            pr_FirstSpawnDelay = Random.Range(pr_FirstSpawnDelayLower, pr_FirstSpawnDelayUpper);
            await Task.Delay(pr_FirstSpawnDelay);
            p_NewSpawnedEnemy = Instantiate(
                p_GroundEnemies[0],
                u_StorageInstantiationPosition.position,
                u_StorageInstantiationPosition.rotation);

            GenericSpawnSetup();

            pr_FirstSpawnDelay = Random.Range(pr_FirstSpawnDelayLower, pr_FirstSpawnDelayUpper);
            await Task.Delay(pr_FirstSpawnDelay);
            p_NewSpawnedEnemy = Instantiate(
                p_FlyingEnemies[0],
                u_StorageInstantiationPosition.position,
                u_StorageInstantiationPosition.rotation);

            GenericSpawnSetup();
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

            GenericSpawnSetup();
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

            GenericSpawnSetup();
        }

        private void GenericSpawnSetup()
        {
            s_NewEnemy = p_NewSpawnedEnemy.GetComponent<Enemy>();
            s_NewEnemyElevationEnum = s_NewEnemy.Elevation();

            u_NewEnemyTransform = p_NewSpawnedEnemy.transform;
            pr_NewEnemyLeftSideSpawn = Random.value > 0.5f;

            ChooseSpawnPosition(u_NewEnemyTransform, s_NewEnemyElevationEnum, pr_NewEnemyLeftSideSpawn);
            s_EnemySetup.Setup(p_NewSpawnedEnemy, this, pr_NewEnemyLeftSideSpawn, pr_DifficultyScalar);
            s_QuipAudio.EnemyQuip(s_NewEnemy.Type());

            p_NewSpawnedEnemy = null;
            s_NewEnemy = null;
            u_NewEnemyTransform = null;
        }

        private void ChooseSpawnPosition(Transform pa_NewEnemyTransform, EnemyElevationEnum pa_EnemyElevation, bool pa_LeftSpawn)
        {
            switch (pa_EnemyElevation)
            {
                case EnemyElevationEnum.FLYING:
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
                case EnemyElevationEnum.GROUND:
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
        }
    }
}
