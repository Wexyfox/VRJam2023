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

        private ObjectRelationsSO.Relation pr_LeftRelation;
        private ObjectRelationsSO.Relation pr_RightRelation;

        private Vector3 pr_NewPostion;
        private Quaternion pr_NewRotation;

        public void NextPlane()
        {
            pr_NewPostion = new Vector3(
                u_LastPlaneTransform.position.x,
                u_LastPlaneTransform.position.y,
                u_LastPlaneTransform.position.z - 10);
            pr_NewRotation = u_LastPlaneTransform.rotation;

            pr_RandomIndex = Random.Range(0, pr_PlanePrefabArray.Length);
            GameObject l_NewPlane = Instantiate(
                pr_PlanePrefabArray[pr_RandomIndex],
                pr_NewPostion,
                pr_NewRotation);
            u_LastPlaneTransform = l_NewPlane.GetComponent<Transform>();

            pr_RandomIndex = Random.Range(0, so_BuildingRelations.Relationships.Length);
            pr_LeftRelation = so_BuildingRelations.Relationships[pr_RandomIndex];
            s_ProjectileSpawner.AddPrefabToSpawnStack(pr_LeftRelation.g_Shootable);

            pr_RandomIndex = Random.Range(0, so_BuildingRelations.Relationships.Length);
            pr_RightRelation = so_BuildingRelations.Relationships[pr_RandomIndex];
            s_ProjectileSpawner.AddPrefabToSpawnStack(pr_RightRelation.g_Shootable);

            s_PlaneSetup.Setup(
                l_NewPlane,
                pr_LeftRelation.g_Environmental,
                pr_LeftRelation.pu_ProjectileEnums,
                pr_LeftRelation.pu_ScoreBonus,
                pr_RightRelation.g_Environmental,
                pr_RightRelation.pu_ProjectileEnums,
                pr_RightRelation.pu_ScoreBonus,
                gameObject.transform,
                this);
        }
    }
}
