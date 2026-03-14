namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_05_EventChannel._01_CSharpEvent
{
    /// <summary>
    /// Marker interface for all event types used with EventChannel.
    ///
    /// This interface is intentionally empty. Its purpose is twofold:
    ///
    /// 1. Constraint — prevents arbitrary types from being used as events by accident.
    ///    EventChannel&lt;T&gt; requires where T : struct, IEvent. Without IEvent, any struct
    ///    could be passed as an event type, making misuse invisible at the call site.
    ///
    /// 2. Extensibility — if the system ever requires a common property or method across
    ///    all event types (a timestamp, a source identifier, a priority level), it can be
    ///    added here without touching each event struct individually. Every event
    ///    automatically gains the new contract by virtue of implementing this interface.
    ///
    /// This is the Marker Interface pattern. It is sometimes considered an anti-pattern
    /// when used purely for runtime type checking — but here it serves a compile-time
    /// constraint and a future extensibility point, both of which are legitimate uses.
    /// </summary>
    public interface IEvent { }
}
