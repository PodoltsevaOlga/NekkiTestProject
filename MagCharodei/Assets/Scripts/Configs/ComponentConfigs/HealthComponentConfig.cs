using Entities;
using EntityComponents;
using UnityEngine;

namespace Configs.ComponentConfigs
{
    [CreateAssetMenu(fileName = "HealthComponentConfig", menuName = "Configs/Components/Health Component")]
    public class HealthComponentConfig : ComponentConfig
    {
        [SerializeField] private int m_MaxHealthPoints;
        public int MaxHealthPoints => m_MaxHealthPoints;
        
        public override EntityComponent CreateComponent(Entity owner)
        {
            return new HealthComponent(owner, MaxHealthPoints);
        }
    }
}