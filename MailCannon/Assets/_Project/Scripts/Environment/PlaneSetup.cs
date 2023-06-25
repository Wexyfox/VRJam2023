using System.Collections.Generic;
using UnityEngine;

namespace VRJam23
{
    public class PlaneSetup : MonoBehaviour
    {
        private Plane s_Plane;
        private PlaneMovement s_PlaneMovement;

        private ScoreZone s_LeftScoreZone;
        private ScoreZone s_RightScoreZone;

        public void Setup(
            GameObject pa_Plane,
            GameObject pa_LeftBuilding,
            List<ProjectileEnum> pa_LeftScoreZoneEnums,
            int pa_LeftScoreBonus,
            GameObject pa_RightObject,
            List<ProjectileEnum> pa_RightScoreZoneEnums,
            int pa_RightScoreBonus,
            Transform pa_SpawnerTransform,
            PlaneSpawner pa_PlaneSpawner)
        {
            s_Plane = pa_Plane.GetComponent<Plane>();
            s_Plane.SpawnerTransformSet(pa_SpawnerTransform, pa_PlaneSpawner);

            if (pa_LeftBuilding != null)
            {
                Instantiate(pa_LeftBuilding, s_Plane.LeftBuildingSpawn().transform);
                s_LeftScoreZone = s_Plane.LeftBuildingSpawn().GetComponent<ScoreZone>();
                s_LeftScoreZone.SetProjectileEnums(pa_LeftScoreZoneEnums);
                s_LeftScoreZone.SetScoreBonus(pa_LeftScoreBonus);
            }

            if (pa_RightObject != null)
            {
                Instantiate(pa_RightObject, s_Plane.RightBuildingSpawn().transform);
                s_RightScoreZone = s_Plane.RightBuildingSpawn().GetComponent<ScoreZone>();
                s_RightScoreZone.SetProjectileEnums(pa_RightScoreZoneEnums);
                s_RightScoreZone.SetScoreBonus(pa_RightScoreBonus);
            }

            s_PlaneMovement = pa_Plane.GetComponent<PlaneMovement>();            
            s_PlaneMovement.StartMoving();
        }
    }
}
