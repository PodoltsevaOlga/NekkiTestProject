using Entities;

namespace Objects.AttackDelivery
{
    public class AttackData
    {
        public int InitialDamage { get; private set; }
        public Entity Caster { get; private set; }
        
        public float CurrentDamage { get; private set; }
        
        //а если есть модификаторы урона, то считаем в оружии заранее
        
        public AttackData(int initialDamage, Entity caster)
        {
            InitialDamage = initialDamage;
            CurrentDamage = initialDamage;
            Caster = caster;
        }

        public void SetCurrentDamage(float damage)
        {
            CurrentDamage = damage;
        }
    }
}