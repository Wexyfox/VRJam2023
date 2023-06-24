using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRJam23
{
    public class BarrelSwivel : MonoBehaviour
    {
        [SerializeField] private Transform u_GrabTarget;

        void Update()
        {
            transform.LookAt(u_GrabTarget);
        }
    }
}
