using System;
using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [Serializable]
    public class WindowDataConfig
    {
        [SerializeField] private List<string> m_WindowIds = new();
        public List<string> WindowIds => m_WindowIds;
    }
}