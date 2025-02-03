using Entities;
using EntityComponents;
using JetBrains.Annotations;
using Objects.AttackDelivery;
using UnityEngine;

namespace Controllers
{
    public static class DealDamageController
    {
        public static bool DealDamage(Entity attacker, Entity target, [CanBeNull] AttackData data)
        {
            if (CanDealDamage(attacker, target))
            {
                var health = target.TryGetComponent<HealthComponent>();
                
                var damageModifiers = target.GetComponents<ReceiveDamageOverrideComponent>();
                if (damageModifiers != null)
                {
                    foreach (var modifier in damageModifiers)
                    {
                        modifier.RecalculateDamage(data);
                    }
                }
                
                health?.TakeDamage(Mathf.FloorToInt(data.CurrentDamage));
            }

            return true;
        }

        public static bool CanDealDamage(Entity attacker, Entity target)
        {
            var health = target.TryGetComponent<HealthComponent>();
            if (health == null)
            {
                return false;
            }
            
            if (attacker is Creature otherCreature && target is Creature ownerCreature &&
                otherCreature.Faction == ownerCreature.Faction)
            {
                return false;
            }

            return true;
        }
    }
}