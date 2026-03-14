namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_06_EventBus
{
    /// <summary>
    /// Marker interface for all event types used with EventBus.
    ///
    /// This interface is intentionally empty. Its purpose is twofold:
    ///
    /// 1. Constraint — prevents arbitrary types from being used as events by accident.
    ///    EventBus methods require where T : struct, IEvent. Without IEvent, any struct
    ///    could be published, making misuse invisible at the call site.
    ///
    /// 2. Extensibility — if the system ever requires a common property or method across
    ///    all event types (a timestamp, a source identifier, a priority level), it can be
    ///    added here without touching each event struct individually.
    ///
    /// This is the Marker Interface pattern. It serves a compile-time constraint and a
    /// future extensibility point — both of which are legitimate uses.
    /// </summary>
    public interface IEvent { }
}
