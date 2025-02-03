using Entities;
using UI;
using UnityEngine;

namespace EntityComponents
{
    public class HealthComponent : EntityComponent, IOnStart
    {
        private int m_MaxHealthPoints;
        private int m_CurrentHealthPoints;
        public bool IsDead => m_CurrentHealthPoints <= 0;
        public bool IsAlive => !IsDead;

        //это временно! юай сейчас вообще не реализован
        private HealthProxy m_HealthBar;

        public HealthComponent(Entity owner, int maxHealthPoints) : base(owner)
        {
            if (maxHealthPoints < 0)
            {
                Debug.LogWarning("Health points is below zero from start");
            }
            
            m_MaxHealthPoints = maxHealthPoints;
            m_CurrentHealthPoints = maxHealthPoints;
        }

        public void TakeDamage(int points)
        {
            if (IsDead)
            {
                return;
            }

            m_CurrentHealthPoints = Mathf.Max(0, m_CurrentHealthPoints - points);
            //TODO: отослать событие про дамаг
            //события должны дергать юай и показывать изменения, но пока что придется напрямую прям отсюда выставлять хелсбар
            m_HealthBar?.SetHealth(m_MaxHealthPoints, m_CurrentHealthPoints);
            
            if (IsDead)
            {
                OnDeath();
            }
        }

        public void Heal(int points)
        {
            m_CurrentHealthPoints = Mathf.Min(m_CurrentHealthPoints + points, m_MaxHealthPoints);
            //TODO: отослать событие на отхил
            m_HealthBar?.SetHealth(m_MaxHealthPoints, m_CurrentHealthPoints);
        }

        private void OnDeath()
        {
            //TODO: отослать событие про смерть
            //пока вместо события и нормального очищения убитых через контроллер просто деактивируемся
            Owner.SetActive(false);
            Owner.View.gameObject.SetActive(false);
        }

        public void OnStart()
        {
            m_HealthBar = Owner.View.GetComponent<HealthProxy>();
            m_HealthBar?.SpawnHealthBar();
            m_HealthBar?.SetHealth(m_MaxHealthPoints, m_CurrentHealthPoints);
        }
    }
}