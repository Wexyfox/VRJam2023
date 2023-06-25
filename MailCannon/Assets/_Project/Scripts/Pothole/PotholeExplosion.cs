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
            GameEvents.Pothole += Pothole;
        }

        private void OnDisable()
        {
            GameEvents.Pothole -= Pothole;
        }

        private void Start()
        {
            u_RigidBodyList = new List<Rigidbody>();
        }

        private void Pothole()
        {
            foreach (Rigidbody l_RigidBody in u_RigidBodyList)
            {
                l_RigidBody.AddExplosionForce(pr_ExplosionForce, transform.position, pr_ExplosionRadius);
            }
        }

        private void OnTriggerEnter(Collider pa_Other)
        {
            if (!pa_Other.GetComponent<Projectile>()) return;
            if (!pa_Other.GetComponent<Rigidbody>()) return;
            u_RigidBodyList.Add(pa_Other.GetComponent<Rigidbody>());
        }

        private void OnTriggerExit(Collider pa_Other)
        {
            if (!pa_Other.GetComponent<Projectile>()) return;
            if (u_RigidBodyList.Contains(pa_Other.GetComponent<Rigidbody>())) return;

            u_RigidBodyList.Remove(pa_Other.GetComponent<Rigidbody>());
        }
    }
}
