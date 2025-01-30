using System;
using Ecs.Buttons;
using MessagePipe;
using MessagePipeEvents;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using VContainer;

namespace Ecs.Windows
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class WindowManagerSystem : ISystem
    {
        public World World { get; set; }
        
        private Filter m_WindowsFilter;
        private Stash<WindowComponent> m_WindowsStash;

        private Stash<PreviousWindowMarker> m_PreviousWindowsStash;
        private Stash<CurrentWindowMarker> m_CurrentWindowsStash;

        private Filter m_ButtonOpenWindowFilter;
        private Stash<ButtonOpenWindowComponent> m_ButtonOpenWindowStash;
        
        private IPublisher<OpenWindowEvent> m_OpenWindowPublisher;
        private ISubscriber<CloseWindowClickedEvent> m_ClickWindowSubscriber;
        
        private IDisposable m_Disposable;
        

        [Inject]
        public void Construct(IPublisher<OpenWindowEvent> publisher,
            ISubscriber<CloseWindowClickedEvent> subscriber)
        {
            m_OpenWindowPublisher = publisher;
            m_ClickWindowSubscriber = subscriber;
        }

        public void OnAwake()
        {
            m_WindowsFilter = World.Filter.With<WindowComponent>().Build();
            m_WindowsStash = World.GetStash<WindowComponent>();

            m_PreviousWindowsStash = World.GetStash<PreviousWindowMarker>();
            m_CurrentWindowsStash = World.GetStash<CurrentWindowMarker>();

            m_ButtonOpenWindowFilter = World.Filter.With<ButtonOpenWindowComponent>().Build();
            m_ButtonOpenWindowStash = World.GetStash<ButtonOpenWindowComponent>();
            
            var bag = DisposableBag.CreateBuilder(); 
            m_ClickWindowSubscriber.Subscribe(ev => OnWindowClosed(ev.WindowId));
            m_Disposable  = bag.Build();
        }

        public void OnUpdate(float deltaTime)
        {
            var buttonOpenWindow = m_ButtonOpenWindowFilter.FirstOrDefault();
            if (buttonOpenWindow != default)
            {
                var previousWindowId = CloseCurrentWindow();

                foreach (var window in m_WindowsFilter)
                {
                    ref var windowComponent = ref m_WindowsStash.Get(window);
                    ref var buttonComponent = ref m_ButtonOpenWindowStash.Get(buttonOpenWindow);
                    if (windowComponent.WindowId == buttonComponent.WindowId)
                    {
                        m_CurrentWindowsStash.Add(window);
                        windowComponent.OpenCount++;
                        m_OpenWindowPublisher.Publish(new OpenWindowEvent()
                        {
                            CurrentWindowId = windowComponent.WindowId, 
                            PreviousWindowId = previousWindowId,
                            OpenCount = windowComponent.OpenCount
                        });
                    }
                }
            }
        }

        private void OnWindowClosed(string windowId)
        {
            CloseCurrentWindow();
        }

        private string? CloseCurrentWindow()
        {
            string? previousWindowId = null;

            foreach (var window in m_WindowsFilter)
            {
                if (m_PreviousWindowsStash.Has(window))
                {
                    m_PreviousWindowsStash.Remove(window);
                    break;
                }
            }
                
            foreach (var window in m_WindowsFilter)
            {
                if (m_CurrentWindowsStash.Has(window))
                {
                    m_CurrentWindowsStash.Remove(window);
                    m_PreviousWindowsStash.Add(window);
                    ref var windowComponent = ref m_WindowsStash.Get(window);
                    previousWindowId = windowComponent.WindowId;
                    break;
                }
            }

            return previousWindowId;
        }
        
        public void Dispose()
        {
            m_Disposable?.Dispose();
        }

    }
}