using Entities;
using EntityComponents;
using UnityEngine;

namespace Configs.ComponentConfigs
{
    [CreateAssetMenu(fileName = "DefenseComponentConfig", menuName = "Configs/Components/Defense Component")]
    public class DefenseComponentConfig : ComponentConfig
    {
        [SerializeField] [Range(0.0f, 1.0f)] private float m_Multiplier;
        public float Multiplier => m_Multiplier;
        
        public override EntityComponent CreateComponent(Entity owner)
        {
            return new DefenseComponent(owner, m_Multiplier);
        }
    }
}