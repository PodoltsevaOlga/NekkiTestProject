using System;
using UnityEngine;

namespace Configs
{
    [Serializable]
    public class UIButtonsConfig
    {
        [SerializeField] private GameObject m_ButtonsPrefab;
        public GameObject ButtonsPrefab => m_ButtonsPrefab;

        [SerializeField] private string m_ButtonText;
        public string ButtonText => m_ButtonText;
    }
}