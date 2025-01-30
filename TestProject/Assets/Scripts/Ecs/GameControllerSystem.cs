using Ecs.Buttons;
using Ecs.Windows;
using MessagePipe;
using MessagePipeEvents;
using Scellecs.Morpeh;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace Ecs
{
    public class GameControllerSystem : IStartable
    {
        private readonly IPublisher<UIInitEvent> m_InitUIPublisher;
        private IObjectResolver m_Container;
        
        private World world;

        public GameControllerSystem(IPublisher<UIInitEvent> publisher, IObjectResolver container)
        {
            m_InitUIPublisher = publisher;
            m_Container = container;
        }

        public void Start()
        { 
            this.world = World.Default;
        
            var systemsGroup = this.world.CreateSystemsGroup();
            
            AddSystem(new WindowSpawnerSystem(), systemsGroup);
            AddSystem(new ButtonHandlerSystem(), systemsGroup);
            AddSystem(new WindowManagerSystem(), systemsGroup);
            AddSystem(new ButtonClickCleanerSystem(), systemsGroup);

            this.world.AddSystemsGroup(order: 0, systemsGroup);

            m_InitUIPublisher.Publish(new UIInitEvent());
        }

        private void AddSystem(ISystem system, SystemsGroup systemsGroup)
        {
            systemsGroup.AddSystem(system);
            m_Container.Inject(system);
        }
    }
}