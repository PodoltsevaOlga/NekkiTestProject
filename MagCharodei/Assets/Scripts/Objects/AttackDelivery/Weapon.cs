using System;
using System.Collections.Generic;
using Configs;
using Entities;
using UnityEngine;

namespace Objects.AttackDelivery
{
    public abstract class Weapon : MonoBehaviour
    {
        public virtual WeaponType WeaponType { get; private set; }

        public Creature Owner { get; private set; }

        public AttackConfig CurrentChosenAttack { get; protected set; }
        
        [SerializeReference] private List<AttackConfig> m_AttackConfigs = new();
        private int m_AttackIndex;

        private float m_LastAttackTime;

        private void Awake()
        {
            m_AttackIndex = 0;
            ChooseAttack(m_AttackConfigs[m_AttackIndex]);
            OnAwake();
        }

        protected virtual void OnAwake()
        {
        }

        public void SetOwner(Creature owner)
        {
            Owner = owner;
        }

        public virtual void Attack()
        {
            if (!CanAttack())
            {
                return;
            }

            m_LastAttackTime = Time.time;
            //TODO: посылаем во вью овнера запуск анимации и всякого?
        }

        protected virtual bool CanAttack()
        {
            if (Time.time - m_LastAttackTime < CurrentChosenAttack.CooldownTime)
            {
                return false;
            }

            return true;
        }

        protected virtual int CalculateDamage(AttackConfig config)
        {
            return config.InitialDamage;
        }

        protected void ChooseAttack(AttackConfig config)
        {
            CurrentChosenAttack = config;
        }

        public void SetPreviousAttack()
        {
            if (m_AttackIndex > 0)
            {
                m_AttackIndex--;
                ChooseAttack(m_AttackConfigs[m_AttackIndex]);
            }
        }
        
        public void SetNextAttack()
        {
            if (m_AttackIndex < m_AttackConfigs.Count - 1)
            {
                m_AttackIndex++;
                ChooseAttack(m_AttackConfigs[m_AttackIndex]);
            }
        }
    }
}