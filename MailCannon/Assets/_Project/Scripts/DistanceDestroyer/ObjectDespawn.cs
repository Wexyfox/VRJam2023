using UnityEngine;

namespace VRJam23
{
    public class ObjectDespawn : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
        }
    }
}
