using JetBrains.Annotations;

namespace MessagePipeEvents
{
    public struct OpenWindowEvent
    {
        public string CurrentWindowId;
        [CanBeNull] public string PreviousWindowId;
        public int OpenCount;
    }
}