using Configs;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VContainer;

namespace UI.Views
{
    public class ButtonOpenWindowView : MonoBehaviour
    {
        [SerializeField] private Button m_Button;
        [SerializeField] private TMP_Text Text;

        [SerializeField] private WindowView m_Window;
        public string? WindowId => m_Window?.WindowId;

        [Inject]
        private UIConfig m_Config;
        
        public UnityAction OnClicked;

        private void Start()
        {
            m_Button.onClick.AddListener(OnClicked);
            Text.text = string.Format(m_Config.ButtonsConfig.ButtonText, WindowId);
        }

        private void OnDestroy()
        {
            m_Button.onClick.RemoveAllListeners();
        }
    }
}

