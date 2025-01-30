using Configs;
using TMPro;
using UnityEngine;
using VContainer;

namespace UI.Views
{
    public class StatusBarView : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_StatusText;
        
        [Inject]
        private UIConfig m_ConfigProvider;
        
        public void UpdateStatus(BarStatus status, string windowId)
        {
            var statusText = string.Format(m_ConfigProvider.StatusBarConfig.GetStatusBarText(status), windowId);
            m_StatusText.text = statusText;
        }
    }
}