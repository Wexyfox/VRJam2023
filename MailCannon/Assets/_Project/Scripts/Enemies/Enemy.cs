using System.Threading.Tasks;
using UnityEngine;

namespace VRJam23
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Rigidbody u_Rigidbody;

        [SerializeField] private int pr_Health;
        [SerializeField] private EnemyTypeEnum pr_TypeEnum;
        [SerializeField] private EnemyStateEnum pr_StateEnum;
        [SerializeField] private float pr_MoveSpeedScalar;
        [SerializeField] private PlaneMovement s_PlaneMovement;
        [SerializeField] private Transform pr_TargetPosition;

        private float pr_DifficultyScalar = 1f;
        private Projectile s_Projectile;

        private int pr_RandomIndex;
        private Transform pr_StartingPosition;
        private Transform[] pr_FarPositions;
        private Transform[] pr_MidPositions;
        private Transform[] pr_ClosePositions;
        private Transform pr_CatchingPosition;

        [SerializeField] private int pr_PositionSwitchDelayLower;
        [SerializeField] private int pr_PositionSwitchDelayUpper;
        private int pr_PositionSwitchDelay;

        [SerializeField] private int pr_DistanceSwitchDelayLower;
        [SerializeField] private int pr_DistanceSwitchDelayUpper;
        private int pr_DistanceSwitchDelay;

        public void Setup(
            Transform pa_StartingPosition,
            Transform[] pa_FarPositions,
            Transform[] pa_MidPositions,
            Transform[] pa_ClosePositions,
            Transform pa_CatchingPosition)
        {
            pr_StateEnum = EnemyStateEnum.ENTERING;

            pr_StartingPosition = pa_StartingPosition;
            pr_FarPositions = pa_FarPositions;
            pr_MidPositions = pa_MidPositions;
            pr_ClosePositions = pa_ClosePositions;
            pr_CatchingPosition = pa_CatchingPosition;

            pr_TargetPosition = pr_StartingPosition;
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

        private void OnTriggerEnter(Collider pa_Other)
        {
            if (pa_Other.gameObject.GetComponent<Projectile>() == null) return;
            s_Projectile = pa_Other.gameObject.GetComponent<Projectile>();

            if (s_Projectile.ProjectileEnum() != ProjectileEnum.JUNK) return;

            s_Projectile.enabled = false;
            pr_Health -= 1;
            HealthCheck();
        }

        private void HealthCheck()
        {
            if (pr_Health > 0) return;

            pr_StateEnum = EnemyStateEnum.EXITING;
            s_PlaneMovement.StartMoving();
        }

        private void FixedUpdate()
        {
            if (pr_StateEnum == EnemyStateEnum.EXITING) return;

            if (pr_StateEnum == EnemyStateEnum.ENTERING && transform.position == pr_StartingPosition.position)
            {
                pr_StateEnum = EnemyStateEnum.CHASINGFAR;
                SelectNewTargetPosition();
            }

            if (transform.position == pr_TargetPosition.position) return;

            u_Rigidbody.AddForce(pr_TargetPosition.position.normalized * pr_MoveSpeedScalar * pr_DifficultyScalar);
        }
    }
}
