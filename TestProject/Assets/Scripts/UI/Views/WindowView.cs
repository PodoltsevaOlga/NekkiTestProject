using Configs;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using VContainer;

namespace UI.Views
{
    public class WindowView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private string m_WindowId;
        [SerializeField] private TMP_Text m_Title;
        [SerializeField] private TMP_Text m_CurrentWindowMessage;
        [SerializeField] private TMP_Text m_PreviousWindowMessage;

        [Inject]
        private UIConfig m_ConfigProvider;

        private void Start()
        {
            m_Title.text = string.Format(m_ConfigProvider.WindowConfig.TitleText, WindowId);
        }

        public string WindowId => m_WindowId;

        public UnityAction OnClick;

        public void Open([CanBeNull] string previousWindowId, int openCount)
        {
            var windowConfig = m_ConfigProvider.WindowConfig;
            m_CurrentWindowMessage.text = string.Format(windowConfig.CurrentWindowMessageText, openCount);
            m_PreviousWindowMessage.text = previousWindowId != null
                ? string.Format(windowConfig.PreviousWindowMessageText, previousWindowId)
                : string.Empty;
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick.Invoke();
        }
    }
}