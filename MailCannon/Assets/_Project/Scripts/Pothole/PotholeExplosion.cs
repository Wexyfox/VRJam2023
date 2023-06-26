using System.Collections.Generic;
using UnityEngine;

namespace VRJam23
{
    public class PotholeExplosion : MonoBehaviour
    {
        [Header("Explosion numbers")]
        [SerializeField] private float pr_ExplosionRadius = 5f;
        [SerializeField] private float pr_ExplosionForce = 1500f;

        private List<Rigidbody> u_RigidBodyList;

        private void OnEnable()
        {
            u_RigidBodyList = new List<Rigidbody>();
            u_RigidBodyList.Clear();
        }

        public void Explode()
        {
            Collider[] l_SurroundingObjects = Physics.OverlapSphere(transform.position, pr_ExplosionRadius);
            
            foreach (Collider l_Collider in l_SurroundingObjects)
            {
                Projectile l_Projectile = l_Collider.gameObject.GetComponent<Projectile>();
                if (l_Projectile == null) continue;

                Rigidbody l_RigidBody = l_Collider.gameObject.GetComponent<Rigidbody>();
                l_RigidBody.AddExplosionForce(pr_ExplosionForce, transform.position, pr_ExplosionRadius);
            }
        }
    }
}
