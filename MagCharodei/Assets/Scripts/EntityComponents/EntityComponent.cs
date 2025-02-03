using Entities;

namespace EntityComponents
{
    public class EntityComponent
    {
        private readonly Entity m_Owner;
        public Entity Owner => m_Owner;

        public EntityComponent(Entity owner)
        {
            m_Owner = owner;
        }
    }
}