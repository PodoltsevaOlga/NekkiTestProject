using Configs.GameConfigs;
using Entities;
using EntityViews;
using UnityEngine;
using Utils;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        private static GameController m_Instance;
        public static GameController Instance => m_Instance;
        
        public readonly PoolFactory ProjectilePools = new();
        
        [SerializeField] private CreatureView m_Player;
        public Creature Player => m_Player?.Data;
        
        [SerializeReference] private KeyCodeConfig m_KeyConfig;
        public KeyCodeConfig KeyConfig => m_KeyConfig;

        [SerializeField] private Canvas m_ScreenSpaceCameraCanvas;
        public Canvas ScreenSpaceCameraCanvas => m_ScreenSpaceCameraCanvas;

        void Awake()
        {
            if (m_Instance != null && m_Instance != this)
            {
                Destroy(this);
            }

            if (m_Instance == null)
            {
                m_Instance = this;
            }
        }
        
        
    }
}