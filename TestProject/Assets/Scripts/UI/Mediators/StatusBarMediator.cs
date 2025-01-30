using System;
using MessagePipe;
using MessagePipeEvents;
using UI.Views;
using VContainer;
using VContainer.Unity;

namespace UI.Mediators
{
    public class StatusBarMediator : IDisposable, IStartable
    {
        private readonly ISubscriber<ButtonOpenWindowClickedEvent> m_ButtonClickedSubscriber;
        private readonly ISubscriber<CloseWindowClickedEvent> m_WindowClickedSubscriber;
        private readonly ISubscriber<UIInitEvent> m_InitSubscriber;

        private StatusBarView m_StatusBarView;

        private IDisposable m_Disposable;
        
        [Inject]
        private readonly UISpawner m_Spawner;

        public StatusBarMediator(ISubscriber<ButtonOpenWindowClickedEvent> subscriberButton,
            ISubscriber<CloseWindowClickedEvent> subscriberWindow,
            ISubscriber<UIInitEvent> subscriberInit)
        {
            m_ButtonClickedSubscriber = subscriberButton;
            m_WindowClickedSubscriber = subscriberWindow;
            m_InitSubscriber = subscriberInit;
        }

        public void Start()
        {
            var bag = DisposableBag.CreateBuilder(); 
            m_ButtonClickedSubscriber.Subscribe(ev => UpdateStatus(BarStatus.OpenedWindow, ev.WindowId));
            m_WindowClickedSubscriber.Subscribe(ev => UpdateStatus(BarStatus.ClosedWindow, ev.WindowId));
            m_InitSubscriber.Subscribe(ev => OnInitUI());
            m_Disposable  = bag.Build();
        }

        private void OnInitUI()
        {
            if (m_StatusBarView == null)
            {
                m_StatusBarView = m_Spawner.SpawnStatusBar();
            }

            UpdateStatus(BarStatus.ProjectStarted, string.Empty);
        }
        
        private void UpdateStatus(BarStatus status, string windowId)
        {
            m_StatusBarView?.UpdateStatus(status, windowId);
        }

        public void Dispose()
        {
            m_Disposable?.Dispose();
        }
    }
}