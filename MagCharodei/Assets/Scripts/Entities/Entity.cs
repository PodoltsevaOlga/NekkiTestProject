using System.Collections.Generic;
using System.Linq;
using Configs;
using Configs.ComponentConfigs;
using EntityComponents;
using EntityViews;

namespace Entities
{
    public class Entity
    {
        private readonly HashSet<EntityComponent> m_Parts = new();
        
        protected EntityView m_View;
        public EntityView View => m_View;
        
        public bool IsActive { get; private set; }

        public Entity(EntityView view)
        {
            m_View = view;
            IsActive = true;
            OnEntityCreated();
        }

        protected virtual void OnEntityCreated()
        {
        }

        public void AddComponentsFromConfig(List<ComponentConfig> configs)
        {
            foreach (var config in configs)
            {
                AddComponent(config.CreateComponent(this));
            }
        }
        
        public bool AddComponent(EntityComponent component)
        {
            if (component == null)
                return false;
            
            return m_Parts.Add(component);
        }

        public bool RemoveComponent(EntityComponent component)
        {
            if (component == null)
                return false;
            
            return m_Parts.Remove(component);
        }

        public T TryGetComponent<T>() where T : EntityComponent
        {
            return m_Parts.FirstOrDefault(comp => comp is T) as T;
        }

        public IEnumerable<T> GetComponents<T>()
        {
            return m_Parts.OfType<T>();
        }

        public void OnUpdate()
        {
            var updateable = GetComponents<IUpdatable>();
            if (updateable != null)
            {
                foreach (var component in updateable)
                {
                    component.OnUpdate();
                }
            }
        }

        public void OnStart()
        {
            var starters = GetComponents<IOnStart>();
            if (starters != null)
            {
                foreach (var component in starters)
                {
                    component.OnStart();
                }
            }
        }

        public void SetActive(bool active)
        {
            IsActive = active;
        }
    }
}