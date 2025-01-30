using System;
using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [Serializable]
    public class UIWindowConfig
    {
        [SerializeField] private List<GameObject> m_WindowPrefabs = new();
        public List<GameObject> WindowPrefabs => m_WindowPrefabs;
        
        [SerializeField] private string m_TitleText;
        [SerializeField] private string m_CurrentWindowMessageText;
        [SerializeField] private string m_PreviousWindowMessageText;

        public string TitleText => m_TitleText;
        public string CurrentWindowMessageText => m_CurrentWindowMessageText;
        public string PreviousWindowMessageText => m_PreviousWindowMessageText;
    }
}