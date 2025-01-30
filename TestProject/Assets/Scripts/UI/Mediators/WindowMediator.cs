using System;
using System.Collections.Generic;
using MessagePipe;
using MessagePipeEvents;
using UI.Views;
using VContainer;
using VContainer.Unity;

namespace UI.Mediators
{
    public class WindowMediator : IDisposable, IStartable
    {
        private readonly ISubscriber<OpenWindowEvent> m_OpenWindowSubscriber;
        private readonly IPublisher<CloseWindowClickedEvent> m_ClickWindowPublisher;
        private readonly ISubscriber<UIInitEvent> m_InitSubscriber;

        private IDisposable m_Disposable;

        private List<WindowView> m_WindowViews;
        private WindowView m_CurrentWindow;
        
        [Inject]
        private readonly UISpawner m_Spawner;
        
        public WindowMediator(ISubscriber<OpenWindowEvent> subscriberOpenWindow, 
            IPublisher<CloseWindowClickedEvent> publisher,
            ISubscriber<UIInitEvent> subscriberInit)
        {
            m_OpenWindowSubscriber = subscriberOpenWindow;
            m_ClickWindowPublisher = publisher;
            m_InitSubscriber = subscriberInit;
        }

        public void Start()
        {
            var bag = DisposableBag.CreateBuilder(); 
            m_OpenWindowSubscriber.Subscribe(OnOpenWindow);
            m_InitSubscriber.Subscribe(ev => OnInitUI());
            m_Disposable  = bag.Build();
        }

        private void OnInitUI()
        {
            if (m_WindowViews == null)
            {
                m_WindowViews = m_Spawner.SpawnWindows();
            }

            foreach (var window in m_WindowViews)
            {
                window.OnClick += (() => OnClickWindow(window));
            }

            m_CurrentWindow = null;
        }

        private void OnOpenWindow(OpenWindowEvent ev)
        {
            if (m_CurrentWindow != null)
            {
                m_CurrentWindow.Close();
            }

            var newWindow = m_WindowViews?.Find(w => w.WindowId == ev.CurrentWindowId);
            if (newWindow != null)
            {
                newWindow.Open(ev.PreviousWindowId, ev.OpenCount);
                m_CurrentWindow = newWindow;
            }
        }

        private void OnClickWindow(WindowView window)
        {
            m_ClickWindowPublisher.Publish(new CloseWindowClickedEvent() {WindowId = m_CurrentWindow.WindowId});
            m_CurrentWindow.Close();
            m_CurrentWindow = null;
        }

        public void Dispose()
        {
            if (m_WindowViews != null)
            {
                foreach (var window in m_WindowViews)
                {
                    window.OnClick -= (() => OnClickWindow(window));
                }
            }

            m_Disposable?.Dispose();
        }
    }
}