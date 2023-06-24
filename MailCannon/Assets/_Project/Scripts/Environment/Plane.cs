using System.Threading.Tasks;
using UnityEngine;

namespace VRJam23
{
    public class Plane : MonoBehaviour
    {
        [SerializeField] private GameObject u_LeftBuilding;
        [SerializeField] private GameObject u_RightBuilding;

        private PlaneSpawner s_PlaneSpawner;
        private Transform u_SpawnerTransform;

        private int pr_PlaneWidthDistance = 10;
        private int pr_MilliSecondDelay = 100;

        public GameObject LeftBuildingSpawn()
        {
            return u_LeftBuilding;
        }

        public GameObject RightBuildingSpawn()
        {
            return u_RightBuilding;
        }

        public void SpawnerTransformSet(Transform pa_SpawnerTransform, PlaneSpawner pa_PlaneSpawner)
        {
            s_PlaneSpawner = pa_PlaneSpawner;
            u_SpawnerTransform = pa_SpawnerTransform;
            DistanceCheckLoop();
        }

        public async void DistanceCheckLoop()
        {
            while (gameObject.activeSelf)
            {
                await Task.Delay(pr_MilliSecondDelay);
                if (transform.position.z - u_SpawnerTransform.position.z > pr_PlaneWidthDistance)
                {
                    s_PlaneSpawner.NextPlane();
                    break;
                }
            }
        }
    }
}
