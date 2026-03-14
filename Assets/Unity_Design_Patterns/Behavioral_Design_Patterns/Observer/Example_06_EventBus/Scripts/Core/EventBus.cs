using System;
using System.Collections.Generic;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_06_EventBus
{
    /// <summary>
    /// A centralized static event bus that decouples publishers from subscribers.
    ///
    /// All event types are managed through a single entry point. Publishers and
    /// subscribers never reference each other — both interact only with EventBus.
    ///
    /// How it works:
    /// Each event type T maps to a delegate in a shared dictionary. When a handler
    /// subscribes to T, it is added to T's delegate chain. When an event is published,
    /// all handlers registered for that type are invoked.
    ///
    /// Compared to EventChannel&lt;T&gt; from Example 05:
    /// EventChannel&lt;T&gt; is a static generic class — each type instantiation is its own
    /// isolated class with its own static field. There is no shared registry.
    /// EventBus is a single static class with a dictionary — all event types live in
    /// one place. This enables centralized management: logging, clearing, and inspecting
    /// all active subscriptions from a single location.
    ///
    /// Constraints — where T : struct, IEvent:
    /// - struct ensures value type semantics: no heap allocation, no null.
    /// - IEvent marks the type as an intentional event, preventing accidental misuse.
    ///
    /// Limitations:
    /// - Static lifetime — subscriptions persist across scene loads unless explicitly cleared.
    /// - No exception isolation — if one subscriber throws, remaining subscribers
    ///   in the same Publish call will not be notified.
    /// - No subscription order guarantee — handlers are invoked in registration order,
    ///   but this should never be relied upon.
    /// - Dictionary lookup adds a small overhead compared to EventChannel&lt;T&gt;,
    ///   which resolves channels at compile time via generic type instantiation.
    /// </summary>
    public static class EventBus
    {
        private static readonly Dictionary<Type, Delegate> _events = new();

        public static void Subscribe<T>(Action<T> handler) where T : struct, IEvent
        {
            var type = typeof(T);

            if (_events.TryGetValue(type, out var existing))
                _events[type] = Delegate.Combine(existing, handler);
            else
                _events[type] = handler;
        }

        public static void Unsubscribe<T>(Action<T> handler) where T : struct, IEvent
        {
            var type = typeof(T);

            if (_events.TryGetValue(type, out var existing))
            {
                var updated = Delegate.Remove(existing, handler);

                if (updated == null)
                    _events.Remove(type);
                else
                    _events[type] = updated;
            }
        }

        public static void Publish<T>(T eventData) where T : struct, IEvent
        {
            var type = typeof(T);

            if (_events.TryGetValue(type, out var existing))
                ((Action<T>)existing)?.Invoke(eventData);
        }

        public static void Clear() => _events.Clear();
    }
}
