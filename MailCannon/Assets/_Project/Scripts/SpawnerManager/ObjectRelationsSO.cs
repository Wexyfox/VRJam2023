using System.Collections.Generic;
using UnityEngine;

namespace VRJam23
{
    [CreateAssetMenu(fileName = "ObjectRelations", menuName = "ScriptableObjects/ObjectRelations")]
    public class ObjectRelationsSO : ScriptableObject
    {
        [System.Serializable]
        public class Relation
        {
            [SerializeField] public List<ProjectileEnum> pu_ProjectileEnums;
            [SerializeField] public GameObject g_Shootable;
            [SerializeField] public GameObject g_Environmental;
            [SerializeField] public int pu_ScoreBonus;
        }

        [SerializeField] public Relation[] Relationships;
    }
}
