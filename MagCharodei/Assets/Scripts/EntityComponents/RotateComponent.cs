using Entities;
using UnityEngine;

namespace EntityComponents
{
    public class RotateComponent : EntityComponent, IUpdatable
    {
        private float m_InitialSpeed;
        private Vector3 m_CurrentDirection;
        
        public RotateComponent(Entity owner, float speed) : base(owner)
        {
            m_InitialSpeed = speed;
            Owner.View.SetRotation(Quaternion.LookRotation(Vector3.up, Vector3.up));
        }

        public void SetDirection(Vector3 direction)
        {
            m_CurrentDirection = direction;
        }

        public void OnUpdate()
        {
            Rotate();
        }


        private void Rotate()
        {
            if (m_CurrentDirection == Vector3.zero)
            {
                return;
            }

            var targetRotation = Quaternion.LookRotation(m_CurrentDirection, Vector3.up);
            Owner.View.SetRotation(Quaternion.Lerp(Owner.View.Rotation, targetRotation, m_InitialSpeed));
        }
    }
}