
using Controllers;
using EntityViews;
using UnityEngine;

namespace Objects.AttackDelivery
{
    [RequireComponent(typeof(Collider2D))]
    public class MeleeWeapon : Weapon
    {
        public override WeaponType WeaponType => WeaponType.MeleeWeapon;

        private Collider2D m_Collider;

        protected override void OnAwake()
        {
            m_Collider = GetComponent<Collider2D>();
            
        }

        public override void Attack()
        {
            base.Attack();
            
            //раз это милишное, то, по мдее, просто послали анимацию телу, с ним движется оружие, с ним движется его коллайдер, ждем соприкосновения
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var entity = other.GetComponent<EntityView>().Data;
            if (entity == null)
            {
                return;
            }

            if (DealDamageController.CanDealDamage(Owner, entity))
            {
                var attack = new AttackData(CalculateDamage(CurrentChosenAttack), Owner);
                DealDamageController.DealDamage(Owner, entity, attack);
            }
            
        }

    }
}