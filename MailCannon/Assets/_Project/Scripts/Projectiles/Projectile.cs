using UnityEngine;

namespace VRJam23
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ProjectileEnum pr_ProjectileEnum;
        [SerializeField] private int pr_PointScore;

        public ProjectileEnum ProjectileEnum()
        {
            return pr_ProjectileEnum;
        }    

        public int Score()
        {
            return pr_PointScore;
        }
    }
}
