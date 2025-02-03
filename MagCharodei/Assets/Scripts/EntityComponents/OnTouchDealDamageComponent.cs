using Controllers;
using Entities;
using Objects.AttackDelivery;

namespace EntityComponents
{
    public class OnTouchDealDamageComponent : EntityComponent, IOnTouch
    {
        public int InitialDamage { get; private set; }
        
        public OnTouchDealDamageComponent(Entity owner, int damage) : base(owner)
        {
            InitialDamage = damage;
        }

        public void OnTouch(Entity entity)
        {
            if (DealDamageController.CanDealDamage(Owner, entity))
            {
                var attackData = new AttackData(InitialDamage, Owner);
                DealDamageController.DealDamage(Owner, entity, attackData);
            }
        }
    }
}