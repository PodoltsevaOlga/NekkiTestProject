using System.Collections.Generic;
using System.Numerics;
using Configs;
using Controllers;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Objects.AttackDelivery
{
    public class RangedWeapon : Weapon
    {
        public override WeaponType WeaponType => WeaponType.RangedWeapon;

        [SerializeField] private Transform m_SpawnProjectilesPoint;

        public new RangedAttackConfig CurrentChosenAttack => (RangedAttackConfig)base.CurrentChosenAttack;
        
        protected override void OnAwake()
        {
            if (m_SpawnProjectilesPoint == null)
            {
                m_SpawnProjectilesPoint = transform;
            }
        }
        
        public override void Attack()
        {
            base.Attack();

            var attackData = new AttackData(CalculateDamage(CurrentChosenAttack), Owner);
            
            var projectileGo =
                GameController.Instance.ProjectilePools.GetOrCreateObject(CurrentChosenAttack.ProjectilePrefab);
            projectileGo.transform.SetPositionAndRotation(m_SpawnProjectilesPoint.transform.position, Quaternion.identity);
            projectileGo.SetActive(true);
            
            var projectile = projectileGo.GetComponent<WeaponProjectile>();
            
            projectile?.SetDirection(Owner.View.Rotation * Vector3.forward);
            projectile?.SetPrefabRef(CurrentChosenAttack.ProjectilePrefab);
            projectile.SetAttackData(attackData);
            projectile.SetSpeed(CurrentChosenAttack.ProjectileSpeed);
        }
    }
}