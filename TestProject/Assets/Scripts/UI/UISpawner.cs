using System.Collections.Generic;
using System.Linq;
using Configs;
using UI.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace UI
{
    public class UISpawner
    {
        private UIConfig m_ConfigProvider;
        private Canvas m_Canvas;
        private IObjectResolver m_Container;

        public UISpawner(IObjectResolver container, UIConfig config, Canvas canvas)
        {
            m_ConfigProvider = config;
            m_Canvas = canvas;
            m_Container = container;
        }
        
        public StatusBarView SpawnStatusBar()
        {
            var go = m_Container.Instantiate(m_ConfigProvider.StatusBarConfig.StatusBarPrefab, m_Canvas.transform);
            return go.GetComponent<StatusBarView>();
        }

        public List<ButtonOpenWindowView> SpawnButtons()
        {
            var go = m_Container.Instantiate(m_ConfigProvider.ButtonsConfig.ButtonsPrefab, m_Canvas.transform);
            return go.GetComponentsInChildren<ButtonOpenWindowView>().ToList();
        }

        public List<WindowView> SpawnWindows()
        {
            var views = new List<WindowView>();
            
            foreach (var prefab in m_ConfigProvider.WindowConfig.WindowPrefabs)
            {
                var go = m_Container.Instantiate(prefab, m_Canvas.transform);
                go.SetActive(false);
                
                var view = go.GetComponent<WindowView>();
                if (view != null)
                {
                    views.Add(view);
                }
            }

            return views;
        }
    }
}