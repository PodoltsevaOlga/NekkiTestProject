using System.Collections.Generic;
using Configs.ComponentConfigs;
using Entities;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "EntityConfig", menuName = "Configs/Entity Config")]
    public class EntityConfig : ScriptableObject
    {
        [SerializeField] private List<ComponentConfig> m_ComponentConfigs = new();
        public List<ComponentConfig> ComponentConfigs => m_ComponentConfigs;

        public virtual void SetupConfig(Entity entity)
        {
            entity.AddComponentsFromConfig(m_ComponentConfigs);
        }
    }
}