using Entities;
using Objects.AttackDelivery;

namespace EntityComponents
{
    //не пригодились, но если есть защита, то сто пудов и усиление будет
    public abstract class DealDamageOverrideComponent : EntityComponent
    {
        public DealDamageOverrideComponent(Entity owner) : base(owner)
        {
        }
        
        public abstract float RecalculateDamage(AttackData attack);
    }
}