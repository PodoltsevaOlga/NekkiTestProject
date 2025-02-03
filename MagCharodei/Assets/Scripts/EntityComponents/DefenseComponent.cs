using Entities;
using Objects.AttackDelivery;

namespace EntityComponents
{
    public class DefenseComponent : ReceiveDamageOverrideComponent
    {
        private float m_Multiplier;
        
        public DefenseComponent(Entity owner, float multiplier) : base(owner)
        {
            m_Multiplier = multiplier;
        }

        public override float RecalculateDamage(AttackData attack)
        {
            attack.SetCurrentDamage(attack.CurrentDamage * m_Multiplier);
            return attack.CurrentDamage;
        }
    }
}