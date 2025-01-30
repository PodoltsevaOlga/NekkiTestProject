using Configs;
using Scellecs.Morpeh;
using VContainer;

namespace Ecs.Windows
{
    public class WindowSpawnerSystem : ISystem
    {
        private IObjectResolver m_Container;
        private GameConfig m_GameConfig;
        
        private Stash<WindowComponent> m_WindowsStash;

        [Inject]
        public void Construct(IObjectResolver container, GameConfig config)
        {
            m_Container = container;
            m_GameConfig = config;
        }
        
        public void OnAwake()
        {
            m_WindowsStash = World.GetStash<WindowComponent>();
            
            foreach (var windowId in m_GameConfig.WindowDataConfig.WindowIds)
            {
                var entity = this.World.CreateEntity();
                m_WindowsStash.Set(entity, new WindowComponent() {WindowId = windowId, OpenCount = 0});
            }
        }
        
        public void Dispose()
        {
        }
        
        public World World { get; set; }
        public void OnUpdate(float deltaTime)
        {
        }
    }
}