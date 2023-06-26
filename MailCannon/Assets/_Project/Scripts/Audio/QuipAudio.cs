using System.Threading.Tasks;
using UnityEngine;

namespace VRJam23
{
    public class QuipAudio : MonoBehaviour
    {
        [Header("Scripts")]
        [SerializeField] private DriverAudio s_DriverAudio;

        [Header("Enemy Quips")]
        [SerializeField] private EnemyQuipSO[] pr_EnemyQuips;

        [Header("Projectile Quips")]
        [SerializeField] private ProjectileQuipSO[] pr_ProjectileQuips;

        private int pr_RandomInt;

        private bool pr_ProjectileQuipable;
        private bool pr_EnemyQuipable;

        [Header("Enemy Quip Delay")]
        [SerializeField] private int pr_ProjectileQuipDelayLower = 12000;
        [SerializeField] private int pr_ProjectileQuipDelayUpper = 25000;
        [SerializeField] private int pr_ProjectileQuipDelay;

        [Header("Projectile Quip Delay")]
        [SerializeField] private int pr_EnemyQuipDelayLower = 15000;
        [SerializeField] private int pr_EnemyQuipDelayUpper = 35000;
        [SerializeField] private int pr_EnemyQuipDelay;

        private void Start()
        {
            pr_ProjectileQuipable = true;
            pr_EnemyQuipable = true;
        }

        public void ProjectileQuip(ProjectileEnum pa_ProjectileType)
        {
            if (!pr_ProjectileQuipable) return;
            if (!s_DriverAudio.Quipable()) return;

            foreach (ProjectileQuipSO l_ProjectileQuipSO in pr_ProjectileQuips)
            {
                if (l_ProjectileQuipSO.pu_ProjectileTypeEnum == pa_ProjectileType)
                {
                    pr_RandomInt = Random.Range(0, l_ProjectileQuipSO.pu_RelatedQuips.Length);
                    s_DriverAudio.Quip(l_ProjectileQuipSO.pu_RelatedQuips[pr_RandomInt]);
                }
            }
            pr_ProjectileQuipable = false;
            ProjectileQuipableReset();
        }

        public void EnemyQuip(EnemyTypeEnum pa_EnemyType)
        {
            if (!pr_EnemyQuipable) return;
            if (!s_DriverAudio.Quipable()) return;

            foreach (EnemyQuipSO l_EnemyQuipSO in pr_EnemyQuips)
            {
                if (l_EnemyQuipSO.pu_EnemyTypeEnum == pa_EnemyType)
                {
                    pr_RandomInt = Random.Range(0, l_EnemyQuipSO.pu_RelatedQuips.Length);
                    s_DriverAudio.Quip(l_EnemyQuipSO.pu_RelatedQuips[pr_RandomInt]);
                }
            }
            pr_EnemyQuipable = false;
            EnemyQuipableReset();
        }

        private async void ProjectileQuipableReset()
        {
            pr_ProjectileQuipDelay = Random.Range(pr_ProjectileQuipDelayLower, pr_ProjectileQuipDelayUpper);
            await Task.Delay(pr_ProjectileQuipDelay);
            pr_ProjectileQuipable = true;
        }

        private async void EnemyQuipableReset()
        {
            pr_EnemyQuipDelayLower = Random.Range(pr_EnemyQuipDelayLower, pr_EnemyQuipDelayUpper);
            await Task.Delay(pr_EnemyQuipDelayLower);
            pr_EnemyQuipable = true;
        }
    }
}
