using Configs.GameConfigs;
using Entities;
using EntityComponents;
using UnityEngine;

namespace Controllers
{
    public class PlayerMoveController : MonoBehaviour
    {
        private Creature m_Player => GameController.Instance.Player; 

        private MoveComponent m_MoveComponent;
        private RotateComponent m_RotateComponent;
        
        private KeyCodeConfig m_KeyConfig;

        private void Start()
        {
            m_MoveComponent = m_Player.TryGetComponent<MoveComponent>();
            m_RotateComponent = m_Player.TryGetComponent<RotateComponent>();
            m_KeyConfig = GameController.Instance.KeyConfig;
        }

        private void Update()
        {
            if (!m_Player.IsActive)
            {
                return;
            }
            
            float inputAxisX = 0.0f;
            float inputAxisY = 0.0f;

            if (Input.GetKey(m_KeyConfig.Up))
            {
                inputAxisY = 1.0f;
            }
            else if (Input.GetKey(m_KeyConfig.Down))
            {
                inputAxisY = -1.0f;
            }
            
            if (Input.GetKey(m_KeyConfig.Left))
            {
                inputAxisX = -1.0f;
            }
            else if (Input.GetKey(m_KeyConfig.Right))
            {
                inputAxisX = 1.0f;
            }
            
            MoveCharacter(new Vector3(inputAxisX, inputAxisY, 0.0f));
        }

        private void MoveCharacter(Vector3 direction)
        {
            m_MoveComponent?.SetDirection(direction);
            m_RotateComponent?.SetDirection(direction);
        }
    }
}