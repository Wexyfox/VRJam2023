using System.Collections.Generic;
using UnityEngine;

namespace VRJam23
{
    public class ScoreZone : MonoBehaviour
    {
        [SerializeField] private List<ProjectileEnum> pr_AcceptedObjects;
        [SerializeField] private int pr_ScoreBonus;
        [SerializeField] private Collider pr_ScoreCollider;

        private Projectile s_Projectile;

        public void SetProjectileEnums(List<ProjectileEnum> pa_ProjectileEnums)
        {
            pr_AcceptedObjects = pa_ProjectileEnums;
        }

        public void SetScoreBonus(int pa_NewScoreBonus)
        {
            pr_ScoreBonus = pa_NewScoreBonus;
        }

        private void OnTriggerEnter(Collider pa_Other)
        {
            if (pa_Other.gameObject.GetComponent<Projectile>() == null) return;
            s_Projectile = pa_Other.gameObject.GetComponent<Projectile>();

            if (!pr_AcceptedObjects.Contains(s_Projectile.ProjectileEnum())) return;

            int l_NewScore = s_Projectile.Score() + pr_ScoreBonus;
            GameEvents.InvokeScoreChange(l_NewScore);

            s_Projectile.enabled = false;
            pr_ScoreCollider.enabled = false;
        }
    }
}
