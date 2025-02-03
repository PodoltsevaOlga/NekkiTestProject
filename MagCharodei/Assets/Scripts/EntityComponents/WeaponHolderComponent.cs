using Entities;
using JetBrains.Annotations;
using Objects.AttackDelivery;

namespace EntityComponents
{
    public class WeaponHolderComponent : EntityComponent
    {
        public Weapon CurrentWeapon { get; private set; }
        
        public WeaponHolderComponent(Entity owner) : base(owner)
        {
        }
        
        public void EquipWeapon(Weapon weapon)
        {
            if (!CanEquip(weapon))
            {
                return;
            }
            
            if (weapon == CurrentWeapon)
            {
                return;
            }
            
            UnequipCurrentWeapon();
            CurrentWeapon = weapon;
            //анимации-шманимации
        }

        public bool CanEquip(Weapon weapon)
        {
            return true;
        }

        public void UnequipCurrentWeapon()
        {
            if (CurrentWeapon == null)
            {
                return;
            }
            
            //анимации-шманимации во вью посылаем

            CurrentWeapon = null;
        }

        public void Attack([CanBeNull] Entity target)
        {
            //в моем опыте для рукопашной атаки просто делали отдельный тип оружия, поэтому поле все равно не нулловое должно быть
            if (CurrentWeapon != null)
            {
                CurrentWeapon.Attack();
            }
        }
    }
}