using EntityComponents;
using UnityEngine;

namespace EntityViews
{
    [RequireComponent(typeof(EntityView))]
    public class FollowAgent : MonoBehaviour
    {
        private MoveComponent m_MoveComponent;
        private RotateComponent m_RotateComponent;
        private EntityView m_View;
        
        private EntityView m_Target;

        //нельзя в Awake, надо дождаться, пока вью создаст энтити
        private void Start()
        {
            m_View = GetComponent<EntityView>();
            m_MoveComponent = m_View?.Data?.TryGetComponent<MoveComponent>();
            m_RotateComponent = m_View?.Data?.TryGetComponent<RotateComponent>();
        }

        public void SetTarget(EntityView entityView)
        {
            m_Target = entityView;
        }

        private void Update()
        {
            var direction = m_Target.Position - m_View.Position;
            m_MoveComponent?.SetDirection(direction);
            m_RotateComponent?.SetDirection(direction);
        }
    }
}