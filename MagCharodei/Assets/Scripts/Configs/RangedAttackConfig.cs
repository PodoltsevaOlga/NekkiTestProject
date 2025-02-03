using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "RangedAttackConfig", menuName = "Configs/Ranged Attack")]
    public class RangedAttackConfig : AttackConfig
    {
        [SerializeField] private GameObject m_ProjectilePrefab;
        public GameObject ProjectilePrefab => m_ProjectilePrefab;

        [SerializeField] private float m_ProjectileSpeed;
        public float ProjectileSpeed => m_ProjectileSpeed;
    }
}