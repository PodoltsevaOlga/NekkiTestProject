using Entities;
using UnityEngine;

namespace EntityComponents
{
    public class MoveComponent : EntityComponent, IUpdatable
    {
        private float m_InitialSpeed;
        private Vector3 m_CurrentDirection;

        public MoveComponent(Entity owner, float speed) : base(owner)
        {
            m_InitialSpeed = speed;
        }

        public void SetDirection(Vector3 direction)
        {
            m_CurrentDirection = direction;
        }

        private void Move()
        {
            Owner.View.SetPosition(Owner.View.Position + m_CurrentDirection * m_InitialSpeed * Time.deltaTime);
        }

        public void OnUpdate()
        {
            Move();
        }
    }
}