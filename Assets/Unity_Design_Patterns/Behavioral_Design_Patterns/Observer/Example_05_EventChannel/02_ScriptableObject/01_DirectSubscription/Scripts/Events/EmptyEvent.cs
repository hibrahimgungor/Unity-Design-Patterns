namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_05_EventChannel._02_ScriptableObject._01_DirectSubscription
{
    /// <summary>
    /// Event struct for notifications that carry no data.
    ///
    /// The occurrence of the event is the meaningful signal — no payload is needed.
    /// QuestStartedEvent and QuestFailedEvent assets both use this type as their channel.
    ///
    /// Why a struct with no fields?
    /// Consistency — all events pass through the same BaseEventChannelSO&lt;T&gt; pipeline.
    /// EmptyEvent satisfies the where T : struct, IEvent constraint without allocating
    /// anything on the heap. The struct itself is zero-sized.
    /// </summary>
    public struct EmptyEvent : IEvent { }
}
