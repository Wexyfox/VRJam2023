using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace VRJam23
{
    public class Turret : MonoBehaviour
    {
        [Header("Stored Transforms")]
        [SerializeField] private Transform u_ObjectStorage;
        [SerializeField] private Transform u_ShootingPoint;

        [Header("Shooting force")]
        [SerializeField] private float pr_ShootingForce = 5f;

        [Header("Scripts")]
        [SerializeField] private QuipAudio s_QuipAudio;

        private GameObject g_LoadedObject;
        private XRGrabInteractable u_XRGrabInteractable;
        private Rigidbody u_LoadedRigidBody;
        private MeshCollider u_LoadedMeshCollider;
        private Projectile s_Projectile;
        private ProjectileEnum pr_ProjectileEnum;

        private void OnTriggerStay(Collider pa_Other)
        {
            if (g_LoadedObject != null) return;
            
            if (pa_Other.gameObject.GetComponent<Projectile>() == null) return;
            
            if (pa_Other.gameObject.GetComponent<XRGrabInteractable>() == null) return;

            if (!pa_Other.gameObject.GetComponent<XRGrabInteractable>().isSelected) return;

            g_LoadedObject = pa_Other.gameObject;
            s_Projectile = g_LoadedObject.GetComponent<Projectile>();
            s_Projectile.Load();

            u_XRGrabInteractable = g_LoadedObject.GetComponent<XRGrabInteractable>();
            u_XRGrabInteractable.GetComponent<XRGrabInteractable>().enabled = false;

            u_LoadedRigidBody = g_LoadedObject.GetComponent<Rigidbody>();
            u_LoadedMeshCollider = g_LoadedObject.GetComponent<MeshCollider>();

            u_LoadedRigidBody.isKinematic = true;
            u_LoadedMeshCollider.enabled = false;

            g_LoadedObject.transform.SetPositionAndRotation(u_ObjectStorage.position, u_ObjectStorage.rotation);
        }

        private void ResetValues()
        {
            u_XRGrabInteractable = null;
            u_LoadedRigidBody = null;
            u_LoadedMeshCollider = null;
            g_LoadedObject = null;
        }

        public void Shoot()
        {
            if (g_LoadedObject == null) return;

            pr_ProjectileEnum = s_Projectile.ProjectileEnum();
            s_QuipAudio.ProjectileQuip(pr_ProjectileEnum);

            g_LoadedObject.transform.SetPositionAndRotation(u_ShootingPoint.position, u_ShootingPoint.rotation);
            u_LoadedMeshCollider.enabled = true;
            u_LoadedRigidBody.isKinematic = false;
            u_LoadedRigidBody.velocity = u_ShootingPoint.forward * pr_ShootingForce;

            ResetValues();
        }
    }
}
