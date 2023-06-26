using System.Threading.Tasks;
using UnityEngine;

namespace VRJam23
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Rigidbody u_Rigidbody;

        [Header("Attributes")]
        [SerializeField] private int pr_Health;
        [SerializeField] private EnemyTypeEnum pr_EnemyTypeEnum;
        [SerializeField] private EnemyElevationEnum pr_ElevationEnum;
        [SerializeField] private EnemyStateEnum pr_StateEnum;
        [SerializeField] private float pr_MoveSpeedScalar;
        [SerializeField] private Collider pr_TriggerCollider;
        [SerializeField] private EnemyAudio s_EnemyAudio;
        [SerializeField] private PlaneMovement s_PlaneMovement;
        [SerializeField] private Transform pr_TargetPosition;

        private float pr_DistanceChecker = 0.2f;

        private bool pr_SetupComplete = false;
        private EnemySpawner s_EnemySpawner;
        private float pr_DifficultyScalar;
        private Projectile s_Projectile;

        private int pr_RandomIndex;
        private Transform pr_StartingPosition;
        private Transform[] pr_FarPositions;
        private Transform[] pr_MidPositions;
        private Transform[] pr_ClosePositions;
        private Transform pr_CatchingPosition;

        [Header("Position Switch Delay")]
        [SerializeField] private int pr_PositionSwitchDelayLower;
        [SerializeField] private int pr_PositionSwitchDelayUpper;
        private int pr_PositionSwitchDelay;

        [Header("Distance Switch Delay")]
        [SerializeField] private int pr_DistanceSwitchDelayLower;
        [SerializeField] private int pr_DistanceSwitchDelayUpper;
        private int pr_DistanceSwitchDelay;

        private void OnEnable()
        {
            GameEvents.GameEnded += GameEnded;
            GameEvents.DifficultyScalarChange += DifficultyScalarChange;
        }

        private void OnDisable()
        {
            GameEvents.GameEnded -= GameEnded;
            GameEvents.DifficultyScalarChange -= DifficultyScalarChange;
        }

        private void GameEnded()
        {
            pr_SetupComplete = false;
            u_Rigidbody.velocity = Vector3.zero;
        }

        private void DifficultyScalarChange(float pa_NewDifficultyScalar)
        {
            pr_DifficultyScalar = pa_NewDifficultyScalar;
        }

        public void Setup(
            Transform pa_StartingPosition,
            Transform[] pa_FarPositions,
            Transform[] pa_MidPositions,
            Transform[] pa_ClosePositions,
            Transform pa_CatchingPosition,
            EnemySpawner pa_EnemySpawner,
            float pa_DifficultyScalar)
        {
            s_EnemySpawner = pa_EnemySpawner;
            pr_StateEnum = EnemyStateEnum.ENTERING;

            pr_DifficultyScalar = pa_DifficultyScalar;

            pr_StartingPosition = pa_StartingPosition;
            pr_FarPositions = pa_FarPositions;
            pr_MidPositions = pa_MidPositions;
            pr_ClosePositions = pa_ClosePositions;
            pr_CatchingPosition = pa_CatchingPosition;

            pr_TargetPosition = pr_StartingPosition;
            NewPositionLoop();
            NewDistanceLoop();
            pr_SetupComplete = true;
        }

        private void SelectNewTargetPosition()
        {
            switch (pr_StateEnum)
            {
                case EnemyStateEnum.ENTERING:
                    pr_TargetPosition = pr_StartingPosition;
                    break;
                case EnemyStateEnum.CHASINGFAR:
                    pr_RandomIndex = Random.Range(0, pr_FarPositions.Length);
                    pr_TargetPosition = pr_FarPositions[pr_RandomIndex];
                    break;
                case EnemyStateEnum.CHASINGMID:
                    pr_RandomIndex = Random.Range(0, pr_MidPositions.Length);
                    pr_TargetPosition = pr_MidPositions[pr_RandomIndex];
                    break;
                case EnemyStateEnum.CHASINGCLOSE:
                    pr_RandomIndex = Random.Range(0, pr_ClosePositions.Length);
                    pr_TargetPosition = pr_ClosePositions[pr_RandomIndex];
                    break;
                case EnemyStateEnum.CATCHINGPLAYER:
                    pr_TargetPosition = pr_CatchingPosition;
                    break;
            }
        }

        private async void NewPositionLoop()
        {
            while (pr_StateEnum != EnemyStateEnum.EXITING || pr_StateEnum != EnemyStateEnum.CATCHINGPLAYER)
            {
                pr_PositionSwitchDelay = Random.Range(pr_PositionSwitchDelayLower, pr_PositionSwitchDelayUpper);
                int l_ModifiedDelay = Mathf.RoundToInt((float)pr_PositionSwitchDelay / pr_DifficultyScalar);
                await Task.Delay(l_ModifiedDelay);

                SelectNewTargetPosition();
            }
        }

        private async void NewDistanceLoop()
        {
            while (pr_StateEnum != EnemyStateEnum.EXITING || pr_StateEnum != EnemyStateEnum.CATCHINGPLAYER)
            {
                pr_DistanceSwitchDelay = Random.Range(pr_DistanceSwitchDelayLower, pr_DistanceSwitchDelayUpper);
                int l_ModifiedDelay = Mathf.RoundToInt((float)pr_DistanceSwitchDelay / pr_DifficultyScalar);
                await Task.Delay(l_ModifiedDelay);

                switch (pr_StateEnum)
                {
                    case EnemyStateEnum.CHASINGFAR:
                        pr_StateEnum = EnemyStateEnum.CHASINGMID;
                        break;
                    case EnemyStateEnum.CHASINGMID:
                        pr_StateEnum = EnemyStateEnum.CHASINGCLOSE;
                        break;
                    case EnemyStateEnum.CHASINGCLOSE:
                        pr_StateEnum = EnemyStateEnum.CATCHINGPLAYER;
                        break;
                }
            }
        }

        private void OnTriggerEnter(Collider pa_Other)
        {
            if (pa_Other.gameObject.GetComponent<Projectile>() == null) return;
            s_Projectile = pa_Other.gameObject.GetComponent<Projectile>();

            s_Projectile.enabled = false;

            if (!s_Projectile.BeenLoaded()) return;

            pr_Health -= 1;
            HealthCheck();
        }

        private void HealthCheck()
        {
            if (pr_Health > 0) return;
            pr_TargetPosition = null;
            pr_TriggerCollider.enabled = false;

            s_EnemyAudio.LoopStop();

            pr_StateEnum = EnemyStateEnum.EXITING;
            switch (pr_ElevationEnum)
            {
                case EnemyElevationEnum.FLYING:
                    s_EnemySpawner.FlyingEnemyDefeated();
                    break;
                case EnemyElevationEnum.GROUND:
                    s_EnemySpawner.GroundEnemyDefeated();
                    break;
            }

            u_Rigidbody.constraints = RigidbodyConstraints.None;
            u_Rigidbody.drag = 1.5f;

            u_Rigidbody.useGravity = true;
            s_PlaneMovement.StartMoving(pr_DifficultyScalar);
        }

        private void FixedUpdate()
        {
            if (!pr_SetupComplete) return;

            if (pr_TargetPosition == null) return;

            if (pr_StateEnum == EnemyStateEnum.ENTERING &&
                (transform.position - pr_StartingPosition.position).magnitude < pr_DistanceChecker)
            {
                pr_StateEnum = EnemyStateEnum.CHASINGFAR;
                SelectNewTargetPosition();
            }

            if (pr_StateEnum == EnemyStateEnum.ENTERING)
            {
                u_Rigidbody.velocity = (
                    pr_TargetPosition.position - transform.position).normalized 
                    * pr_MoveSpeedScalar 
                    * pr_DifficultyScalar 
                    * 5f;
                return;
            }

            if (pr_StateEnum == EnemyStateEnum.EXITING) return;

            if ((transform.position - pr_TargetPosition.position).magnitude < pr_DistanceChecker)
            {
                u_Rigidbody.velocity = Vector3.zero;
                return;
            } 

            u_Rigidbody.velocity = (
                    pr_TargetPosition.position - transform.position).normalized 
                    * pr_MoveSpeedScalar 
                    * pr_DifficultyScalar;
        }

        public EnemyElevationEnum Elevation()
        {
            return pr_ElevationEnum;
        }

        public EnemyTypeEnum Type()
        {
            return pr_EnemyTypeEnum;
        }
    }
}
