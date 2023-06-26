using UnityEngine;

namespace VRJam23
{
    [CreateAssetMenu(fileName = "EnemyQuipSO", menuName = "ScriptableObjects/EnemyQuipSO")]

    public class EnemyQuipSO : ScriptableObject
    {
        public EnemyTypeEnum pu_EnemyTypeEnum;
        public AudioClip[] pu_RelatedQuips;
    }
}
