using UnityEngine;

namespace VRJam23
{
    public class PlaneSetup : MonoBehaviour
    {
        private Plane s_Plane;
        private PlaneMovement s_PlaneMovement;

        public void Setup(
            GameObject pa_Plane, 
            GameObject pa_LeftBuilding, 
            GameObject pa_RightObject,
            Transform pa_SpawnerTransform,
            PlaneSpawner pa_PlaneSpawner)
        {
            s_Plane = pa_Plane.GetComponent<Plane>();
            s_Plane.SpawnerTransformSet(pa_SpawnerTransform, pa_PlaneSpawner);

            Instantiate(pa_LeftBuilding, s_Plane.LeftBuildingSpawn());
            Instantiate(pa_RightObject, s_Plane.RightBuildingSpawn());

            s_PlaneMovement = pa_Plane.GetComponent<PlaneMovement>();            
            s_PlaneMovement.StartMoving();
        }
    }
}
