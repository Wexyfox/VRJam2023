using UnityEngine;

namespace VRJam23
{
    [CreateAssetMenu(fileName = "ProjectileQuipSO", menuName = "ScriptableObjects/ProjectileQuipSO")]
    public class ProjectileQuipSO : ScriptableObject
    {
        public ProjectileEnum pu_ProjectileTypeEnum;
        public AudioClip[] pu_RelatedQuips;
    }
}

