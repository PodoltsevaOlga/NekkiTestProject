using Entities;
using Objects.AttackDelivery;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "CreatureConfig", menuName = "Configs/Creature Config")]
    public class CreatureConfig : EntityConfig
    {
        [SerializeField] private GameObject m_InitialWeapon;
        public GameObject InitialWeapon => m_InitialWeapon;

        [SerializeField] private Faction m_Faction;
        public Faction Faction => m_Faction;

        public override void SetupConfig(Entity entity)
        {
            base.SetupConfig(entity);

            if (entity is not Creature creature)
            {
                return;
            }

            creature.SetFaction(m_Faction);

            if (m_InitialWeapon != null)
            {
                var go = GameObject.Instantiate(m_InitialWeapon);
                go.transform.SetParent(creature.View.transform);

                var weapon = go.GetComponent<Weapon>();
                weapon.SetOwner(creature);
                creature.WeaponHolderComponent.EquipWeapon(weapon);
            }
        }
    }
}