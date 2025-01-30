using System;
using System.Collections.Generic;
using MessagePipe;
using MessagePipeEvents;
using UI.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace UI.Mediators
{
    public class ButtonMediator : IDisposable, IStartable
    {
        private List<ButtonOpenWindowView> m_ButtonViews;
        
        private readonly IPublisher<ButtonOpenWindowClickedEvent> m_ButtonClickPublisher;
        private readonly ISubscriber<UIInitEvent> m_InitSubscriber;

        private IDisposable m_Disposable;

        [Inject]
        private readonly UISpawner m_Spawner;
        
        public ButtonMediator(IPublisher<ButtonOpenWindowClickedEvent> publisher,
            ISubscriber<UIInitEvent> subscriber)
        {
            m_ButtonClickPublisher = publisher;
            m_InitSubscriber = subscriber;
        }
        
        public void Start()
        {
            var bag = DisposableBag.CreateBuilder(); 
            m_InitSubscriber.Subscribe(ev => OnInitUI());
            m_Disposable  = bag.Build();
        }

        private void OnInitUI()
        {
            if (m_ButtonViews == null)
            {
                m_ButtonViews = m_Spawner.SpawnButtons();
            }

            foreach (var buttonView in m_ButtonViews)
            {
                buttonView.OnClicked += (() => OnClickButtonAction(buttonView));
            }
        }
        
        private void OnClickButtonAction(ButtonOpenWindowView view)
        {
            if (view.WindowId == null)
            {
                Debug.LogWarning($"Button {view.name} doesn't have window attached");
            }
            else
            {
                m_ButtonClickPublisher.Publish(new ButtonOpenWindowClickedEvent() {WindowId = view.WindowId});
            }
            
        }

        public void Dispose()
        {
            m_Disposable?.Dispose();
        }
    }
}