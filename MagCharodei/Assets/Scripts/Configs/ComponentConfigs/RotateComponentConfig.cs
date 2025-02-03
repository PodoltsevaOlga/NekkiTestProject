using Entities;
using EntityComponents;
using UnityEngine;

namespace Configs.ComponentConfigs
{
    [CreateAssetMenu(fileName = "RotateComponentConfig", menuName = "Configs/Components/Rotate Component")]
    public class RotateComponentConfig : ComponentConfig
    {
        [SerializeField] private float m_RotationSpeed;
        public float RotationSpeed => m_RotationSpeed;
        
        public override EntityComponent CreateComponent(Entity owner)
        {
            return new RotateComponent(owner, m_RotationSpeed);
        }
    }
}