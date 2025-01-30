using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Ecs.Buttons
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class ButtonClickCleanerSystem : ISystem
    {
        public World World { get; set; }

        private Filter m_ButtonOpenWindowFilter;

        public void OnAwake()
        {
            m_ButtonOpenWindowFilter = World.Filter.With<ButtonOpenWindowComponent>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var button in m_ButtonOpenWindowFilter)
            {
                World.RemoveEntity(button);
            }
        }

        public void Dispose()
        {
        }
    }
}