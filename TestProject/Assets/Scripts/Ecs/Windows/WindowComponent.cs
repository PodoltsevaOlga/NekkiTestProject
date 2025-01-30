using Scellecs.Morpeh;

namespace Ecs.Windows
{
    public struct WindowComponent : IComponent
    {
        public string WindowId;
        public int OpenCount;
    }
}