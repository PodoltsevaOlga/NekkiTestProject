using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Game Config", menuName = "Configs/Game Config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private WindowDataConfig m_WindowDataConfig;
        public WindowDataConfig WindowDataConfig => m_WindowDataConfig;
    }
}