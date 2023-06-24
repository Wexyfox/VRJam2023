using UnityEngine;

namespace VRJam23
{
    public class FirstPlaneSetup : MonoBehaviour
    {
        [SerializeField] private Plane s_FirstPlane;
        [SerializeField] private PlaneSpawner u_Spawner;

        private void Start()
        {
            s_FirstPlane.SpawnerTransformSet(u_Spawner.transform, u_Spawner);
            GameEvents.InvokeGameStart();
        }
    }
}
