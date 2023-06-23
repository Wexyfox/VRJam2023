using UnityEngine;

namespace VRJam23
{
    public class Grabber : MonoBehaviour
    {
        [SerializeField] private Grabber s_OtherGrab;

        [SerializeField] private GameObject pr_DetectedObject = null;
        [SerializeField] private GameObject pr_GrabbedObject = null;
        [SerializeField] private bool pr_Grabbing;

        private Rigidbody u_GrabbedRigidbody;

        private void Start()
        {
            pr_Grabbing = false;
        }

        private void OnTriggerEnter(Collider pa_Other)
        {
            pr_DetectedObject = pa_Other.gameObject;
        }

        private void OnTriggerStay(Collider pa_Other)
        {
            pr_DetectedObject = pa_Other.gameObject;
        }

        private void OnTriggerExit(Collider pa_Other)
        {
            if (pr_DetectedObject != pa_Other.gameObject) return;
            pr_DetectedObject = null;
        }

        public void GrabObject()
        {
            if (pr_DetectedObject == null) return;

            if (pr_Grabbing) return;

            if (s_OtherGrab.HeldObject() == pr_DetectedObject) return;

            if (!pr_DetectedObject.GetComponent<Projectile>()) return;

            pr_GrabbedObject = pr_DetectedObject;
            u_GrabbedRigidbody = pr_GrabbedObject.GetComponent<Rigidbody>();

            
            pr_GrabbedObject.transform.SetParent(this.gameObject.transform);
            u_GrabbedRigidbody.useGravity = false;
            u_GrabbedRigidbody.velocity = Vector3.zero;
            pr_Grabbing = true;
        }

        public void ReleaseObject()
        {
            if (pr_GrabbedObject == null) return;

            pr_GrabbedObject.transform.SetParent(null);
            u_GrabbedRigidbody.useGravity = true;

            u_GrabbedRigidbody = null;

            pr_GrabbedObject = null;
            pr_DetectedObject = null;

            pr_Grabbing = false;
        }

        public GameObject HeldObject()
        {
            return pr_GrabbedObject;
        }
    }
}
