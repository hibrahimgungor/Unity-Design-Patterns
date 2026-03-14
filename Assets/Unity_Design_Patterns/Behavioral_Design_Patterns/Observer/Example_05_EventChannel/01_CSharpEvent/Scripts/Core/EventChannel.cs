using System;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_05_EventChannel._01_CSharpEvent
{
    /// <summary>
    /// A static generic event channel that decouples publishers from subscribers.
    ///
    /// Each event type T gets its own isolated channel. Publishing a QuestCompletedEvent
    /// only notifies subscribers of QuestCompletedEvent — no other event type is affected.
    ///
    /// Because the class is static and generic, EventChannel&lt;QuestCompletedEvent&gt; and
    /// EventChannel&lt;QuestStartedEvent&gt; are entirely separate — they do not share state.
    /// This is the key distinction from a traditional EventBus: there is no central registry
    /// or dictionary lookup. Each generic type instantiation is its own isolated channel.
    ///
    ///
    /// Constraint — where T : struct, IEvent:
    /// - struct ensures value type semantics, no heap allocation, no null.
    /// - IEvent marks the type as an intentional event, preventing accidental misuse.
    ///   It is currently empty but serves as a future extensibility point — any common
    ///   property or behaviour needed across all events can be added to IEvent without
    ///   modifying each event struct individually.
    ///
    /// Limitations of this simple implementation:
    /// - Static events persist for the application lifetime. Subscribers that forget
    ///   to unsubscribe remain registered across scene loads.
    /// - No exception isolation — if one subscriber throws, remaining subscribers
    ///   in the same Raise call will not be notified.
    /// - No subscription tracking — there is no way to inspect who is currently subscribed.
    /// These limitations are addressed in 02_UnityEvent and 03_ScriptableObject.
    /// </summary>
    public static class EventChannel<T> where T : struct, IEvent
    {
        private static event Action<T> _event;

        public static void Subscribe(Action<T> handler) => _event += handler;
        public static void Unsubscribe(Action<T> handler) => _event -= handler;
        public static void Raise(T eventData) => _event?.Invoke(eventData);
        public static void Clear() => _event = null;
    }
}
