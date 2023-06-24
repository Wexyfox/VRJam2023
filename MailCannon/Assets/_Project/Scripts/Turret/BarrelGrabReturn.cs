using UnityEngine;

namespace VRJam23
{
    public class BarrelGrabReturn : MonoBehaviour
    {
        [SerializeField] private Transform u_ReturnTransform;
        [SerializeField] private Rigidbody u_RigidBody;

        public void Return()
        {
            gameObject.transform.SetPositionAndRotation(u_ReturnTransform.position, u_ReturnTransform.rotation);
            u_RigidBody.velocity = Vector3.zero;
        }
    }
}
