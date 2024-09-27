namespace UIFramework.UIEvent
{
    public class GlobalDragUpdateAction : CustomEvent
    {
        public static readonly int EventId = typeof(GlobalDragUpdateAction).GetHashCode();
        public override int Id => EventId;
    }
    public class GlobalDragExitedAction : CustomEvent
    {
        public static readonly int EventId = typeof(GlobalDragExitedAction).GetHashCode();
        public override int Id => EventId;
    }
}