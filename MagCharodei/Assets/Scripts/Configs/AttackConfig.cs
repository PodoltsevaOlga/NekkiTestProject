using UnityEngine;

namespace Configs
{
    public abstract class AttackConfig : ScriptableObject
    {
        [SerializeField] private int m_InitialDamage;
        public int InitialDamage => m_InitialDamage;

        [SerializeField] private float m_CooldownTime;
        public float CooldownTime => m_CooldownTime;

    }
}