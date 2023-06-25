using UnityEngine;

namespace VRJam23
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ProjectileEnum pr_ProjectileEnum;
        [SerializeField] private int pr_PointScore;

        private bool pr_Loaded = false;

        public ProjectileEnum ProjectileEnum()
        {
            return pr_ProjectileEnum;
        }    

        public int Score()
        {
            return pr_PointScore;
        }

        public void Load()
        {
            pr_Loaded = true;
        }

        public bool BeenLoaded()
        {
            return pr_Loaded;
        }
    }
}
