using System;
using MessagePipe;
using MessagePipeEvents;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using VContainer;

namespace Ecs.Buttons
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class ButtonHandlerSystem : ISystem
    {
        public World World { get; set; }
        
        private ISubscriber<ButtonOpenWindowClickedEvent> m_EventSubscriber;
        private IDisposable m_Disposable;
        private Stash<ButtonOpenWindowComponent> m_ButtonStash;

        [Inject]
        public void Construct(ISubscriber<ButtonOpenWindowClickedEvent> subscriber)
        {
            m_EventSubscriber = subscriber;
        }

        public void OnAwake()
        {
            var bag = DisposableBag.CreateBuilder(); 
            m_EventSubscriber.Subscribe(ev => OnButtonOpenWindowClicked(ev.WindowId));
            m_Disposable  = bag.Build();

            m_ButtonStash = World.GetStash<ButtonOpenWindowComponent>();
        }

        private void OnButtonOpenWindowClicked(string windowId)
        {
            var eventEntity = World.CreateEntity();
            ref var buttonClicked = ref m_ButtonStash.Add(eventEntity);
            buttonClicked.WindowId = windowId;
        }
        
        public void OnUpdate(float deltaTime)
        {
        }
        
        public void Dispose()
        {
            m_Disposable?.Dispose();
        }
    }
}