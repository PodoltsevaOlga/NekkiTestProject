using Entities;
using Objects.AttackDelivery;

namespace EntityComponents
{
    public abstract class ReceiveDamageOverrideComponent : EntityComponent
    {
        public ReceiveDamageOverrideComponent(Entity owner) : base(owner)
        {
        }

        public abstract float RecalculateDamage(AttackData attack);
    }
}