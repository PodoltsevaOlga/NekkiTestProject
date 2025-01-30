using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "UI Config", menuName = "Configs/UI Config")]
    public class UIConfig : ScriptableObject
    { 
        [SerializeField] private UIStatusBarConfig m_StatusBarConfig;
        public UIStatusBarConfig StatusBarConfig => m_StatusBarConfig;

        [SerializeField] private UIWindowConfig m_WindowConfig;
        public UIWindowConfig WindowConfig => m_WindowConfig;

        [SerializeField] private UIButtonsConfig m_ButtonsConfig;
        public UIButtonsConfig ButtonsConfig => m_ButtonsConfig;
    }
}