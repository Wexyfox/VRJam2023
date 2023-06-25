using UnityEngine;

namespace VRJam23
{
    public class EnemySetup : MonoBehaviour
    {
        [Header("Flying Positions")]
        [SerializeField] private Transform u_FlyingLeftStartingPosition;
        [SerializeField] private Transform u_FlyingRightStartingPosition;
        [SerializeField] private Transform[] u_FlyingFarPositions;
        [SerializeField] private Transform[] u_FlyingMidPositions;
        [SerializeField] private Transform[] u_FlyingClosePositions;
        [SerializeField] private Transform u_FlyingCatchingPosition;

        [Header("Ground Positions")]
        [SerializeField] private Transform u_GroundLeftStartingPosition;
        [SerializeField] private Transform u_GroundRightStartingPosition;
        [SerializeField] private Transform[] u_GroundFarPositions;
        [SerializeField] private Transform[] u_GroundMidPositions;
        [SerializeField] private Transform[] u_GroundClosePositions;
        [SerializeField] private Transform u_GroundCatchingPosition;

        private Enemy s_Enemy;

        public void Setup(GameObject pa_NewEnemy, EnemySpawner pa_EnemySpawner, bool pa_LeftSpawn, float pa_DifficultyScalar)
        {
            s_Enemy = pa_NewEnemy.GetComponent<Enemy>();
            switch (s_Enemy.Type())
            {
                case EnemyTypeEnum.FLYING:
                    FlyingSetup(pa_EnemySpawner, pa_LeftSpawn, pa_DifficultyScalar);
                    break;
                case EnemyTypeEnum.GROUND:
                    GroundSetup(pa_EnemySpawner, pa_LeftSpawn, pa_DifficultyScalar);
                    break;
            }
        }

        private void FlyingSetup(EnemySpawner pa_EnemySpawner, bool pa_LeftSpawn, float pa_DifficultyScalar)
        {
            Transform l_StartingTransform;
            if (pa_LeftSpawn) l_StartingTransform = u_FlyingLeftStartingPosition;
            else l_StartingTransform = u_FlyingRightStartingPosition;

            s_Enemy.Setup(
                l_StartingTransform,
                u_FlyingFarPositions,
                u_FlyingMidPositions,
                u_FlyingClosePositions,
                u_FlyingCatchingPosition,
                pa_EnemySpawner,
                pa_DifficultyScalar);
        }

        private void GroundSetup(EnemySpawner pa_EnemySpawner, bool pa_LeftSpawn, float pa_DifficultyScalar)
        {
            Transform l_StartingTransform;
            if (pa_LeftSpawn) l_StartingTransform = u_GroundLeftStartingPosition;
            else l_StartingTransform = u_GroundRightStartingPosition;

            s_Enemy.Setup(
                l_StartingTransform,
                u_GroundFarPositions,
                u_GroundMidPositions,
                u_GroundClosePositions,
                u_GroundCatchingPosition,
                pa_EnemySpawner,
                pa_DifficultyScalar);
        }
    }
}
