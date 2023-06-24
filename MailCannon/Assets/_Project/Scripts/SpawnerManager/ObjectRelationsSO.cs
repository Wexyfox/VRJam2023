using UnityEngine;

namespace VRJam23
{
    [CreateAssetMenu(fileName = "ObjectRelations", menuName = "ScriptableObjects/ObjectRelations")]
    public class ObjectRelationsSO : ScriptableObject
    {
        [System.Serializable]
        public class Relation
        {
            [SerializeField] public GameObject g_Shootable;
            [SerializeField] public GameObject g_Environmental;
        }

        [SerializeField] public Relation[] Relationships;
    }
}
