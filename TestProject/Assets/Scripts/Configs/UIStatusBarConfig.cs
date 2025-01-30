using System;
using UI;
using UnityEngine;

namespace Configs
{
    [Serializable]
    public class UIStatusBarConfig
    {
        [SerializeField] private GameObject m_StatusBarPrefab;
        public GameObject StatusBarPrefab => m_StatusBarPrefab;
        
        [SerializeField] private string m_ProjectStartedStatusText;
        [SerializeField] private string m_OpenedWindowStatusText;
        [SerializeField] private string m_ClosedWindowStatusText;
        
        public string ProjectStartedStatusText => m_ProjectStartedStatusText;
        public string OpenedWindowStatusText => m_OpenedWindowStatusText;
        public string ClosedWindowStatusText => m_ClosedWindowStatusText;
        
        public string GetStatusBarText(BarStatus status)
        {
            switch (status)
            {
                case BarStatus.ProjectStarted:
                    return ProjectStartedStatusText;
                case BarStatus.OpenedWindow:
                    return OpenedWindowStatusText;
                case BarStatus.ClosedWindow:
                    return ClosedWindowStatusText;
                default:
                    throw new NotImplementedException($"No {status} found");
            }
        }
    }
}