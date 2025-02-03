using Controllers;
using EntityViews;
using UnityEngine;

namespace Objects.AttackDelivery
{
    [RequireComponent(typeof(Collider2D))]
    public class WeaponProjectile : MonoBehaviour
    {
        private Collider2D m_Collider;
        public GameObject PrefabRef { get; private set; }
        public AttackData AttackData { get; private set; }

        private float m_Speed;
        private Vector3 m_MoveDirection;

        private const float m_DestroyRange = 100.0f; 

        private void Awake()
        {
            m_Collider = GetComponent<Collider2D>();
        }

        public void SetPrefabRef(GameObject prefab)
        {
            PrefabRef = prefab;
        }

        public void SetAttackData(AttackData attackData)
        {
            AttackData = attackData;
        }

        public void SetDirection(Vector3 direction)
        {
            m_MoveDirection = direction;
        }

        public void SetSpeed(float speed)
        {
            m_Speed = speed;
        }

        private void OnCollisionEnter2D(Collision2D collider)
        {
            //в настройке физики проекта выставим, обо что можно ударяться
            var entity = collider.gameObject.GetComponent<EntityView>().Data;
            
            if (entity != null)
            {
                if (DealDamageController.CanDealDamage(AttackData.Caster, entity))
                {
                    DealDamageController.DealDamage(AttackData.Caster, entity, AttackData);
                }
            }
            
            BurstOnCollide();
        }

        private void BurstOnCollide()
        {
            //TODO: пыщ-бух-звуки
            Reset();
        }

        private void Update()
        {
            transform.position += m_MoveDirection * m_Speed * Time.deltaTime;
            
            var screenPos = Camera.main.WorldToScreenPoint(transform.position);
            bool notDestroy = screenPos.x > 0f - m_DestroyRange && screenPos.x < Screen.width + m_DestroyRange &&
                              screenPos.y > 0f - m_DestroyRange && screenPos.y < Screen.height + m_DestroyRange;
            if (!notDestroy)
            {
                Reset();
            }

        }

        private void Reset()
        {
            gameObject.SetActive(false);
            transform.position = Vector3.zero;
            GameController.Instance.ProjectilePools.ReleaseObject(gameObject, PrefabRef);
        }
    }
}