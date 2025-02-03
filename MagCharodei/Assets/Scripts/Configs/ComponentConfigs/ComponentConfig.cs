using Entities;
using EntityComponents;
using UnityEngine;

namespace Configs.ComponentConfigs
{
    public abstract class ComponentConfig : ScriptableObject
    {
        public abstract EntityComponent CreateComponent(Entity owner);
    }
}