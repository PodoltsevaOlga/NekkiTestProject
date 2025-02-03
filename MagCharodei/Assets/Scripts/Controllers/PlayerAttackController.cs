using Configs.GameConfigs;
using Entities;
using UnityEngine;

namespace Controllers
{
    public class PlayerAttackController : MonoBehaviour
    {
        private Creature m_Player => GameController.Instance.Player;
        private KeyCodeConfig m_KeyConfig;

        private void Start()
        {
            m_KeyConfig = GameController.Instance.KeyConfig;
        }

        private void Update()
        {
            if (!m_Player.IsActive)
            {
                return;
            }
            
            if (Input.GetKeyDown(m_KeyConfig.CastSpell))
            {
                m_Player.WeaponHolderComponent.Attack(null);
            }
        }
    }
}