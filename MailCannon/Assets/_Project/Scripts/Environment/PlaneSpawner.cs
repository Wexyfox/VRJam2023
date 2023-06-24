using UnityEngine;

namespace VRJam23
{
    public class PlaneSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] pr_PlanePrefabArray;
        [SerializeField] private ObjectRelationsSO so_BuildingRelations;
        [SerializeField] private PlaneSetup s_PlaneSetup;
        [SerializeField] private ProjectileSpawner s_ProjectileSpawner;

        [SerializeField] private Transform u_FirstPlane;

        private Transform u_LastPlaneTransform;

        private int pr_RandomIndex;

        private void Start()
        {
            u_LastPlaneTransform = u_FirstPlane;
        }

        public void NextPlane()
        {            
            Vector3 l_NewPosition = new Vector3(
                u_LastPlaneTransform.position.x,
                u_LastPlaneTransform.position.y,
                u_LastPlaneTransform.position.z - 10);
            Quaternion l_NewRotation = u_LastPlaneTransform.rotation;

            pr_RandomIndex = Random.Range(0, pr_PlanePrefabArray.Length);
            GameObject l_NewPlane = Instantiate(
                pr_PlanePrefabArray[pr_RandomIndex],
                l_NewPosition,
                l_NewRotation);
            u_LastPlaneTransform = l_NewPlane.GetComponent<Transform>();

            pr_RandomIndex = Random.Range(0, so_BuildingRelations.Relationships.Length);
            GameObject l_LeftBuilding = so_BuildingRelations.Relationships[pr_RandomIndex].g_Environmental;
            s_ProjectileSpawner.AddPrefabToSpawnStack(so_BuildingRelations.Relationships[pr_RandomIndex].g_Shootable);

            pr_RandomIndex = Random.Range(0, so_BuildingRelations.Relationships.Length);
            GameObject l_RightBuilding = so_BuildingRelations.Relationships[pr_RandomIndex].g_Environmental;
            s_ProjectileSpawner.AddPrefabToSpawnStack(so_BuildingRelations.Relationships[pr_RandomIndex].g_Shootable);

            s_PlaneSetup.Setup(l_NewPlane, l_LeftBuilding, l_RightBuilding, gameObject.transform, this);
        }
    }
}
