using Entities;
using EntityComponents;
using UnityEngine;

namespace Configs.ComponentConfigs
{
    [CreateAssetMenu(fileName = "MoveComponentConfig", menuName = "Configs/Components/Move Component")]
    public class MoveComponentConfig : ComponentConfig
    {
        [SerializeField] private float m_Speed;
        public float Speed => m_Speed;
        
        public override EntityComponent CreateComponent(Entity owner)
        {
            return new MoveComponent(owner, m_Speed);
        }
    }
}