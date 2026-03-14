using System;
using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_05_EventChannel._02_ScriptableObject._01_DirectSubscription
{
    /// <summary>
    /// Abstract base class for all ScriptableObject-based event channels.
    ///
    /// Each instance of a derived channel is an independent asset with its own
    /// subscriber list. Unlike the static EventChannel&lt;T&gt; in 01_CSharpEvent,
    /// the event field here is non-static — two assets of the same type do not
    /// share subscribers.
    ///
    /// This is the key advantage of the ScriptableObject approach:
    /// the channel is a first-class asset that can be created, named, and assigned
    /// in the Inspector. Publishers and subscribers both hold a reference to the
    /// same asset — not to each other.
    ///
    /// Why abstract?
    /// This class cannot be instantiated directly because it carries no
    /// CreateAssetMenu attribute and has no meaningful standalone use.
    /// Concrete subclasses provide the CreateAssetMenu attribute and fix the
    /// generic type parameter, making them the actual assets that appear in
    /// the Project window.
    ///
    /// No constraint on T:
    /// Earlier implementations used where T : struct, IEvent to enforce value
    /// type semantics and prevent accidental misuse. That constraint is intentionally
    /// removed here — IntEventChannelSO passes int directly as T, and int does not
    /// implement IEvent. The trade-off: less compile-time protection, simpler API.
    /// EmptyEventChannelSO uses the Empty struct to satisfy the generic parameter
    /// for channels that carry no data — see Empty.cs for the reasoning.
    /// </summary>
    public abstract class BaseEventChannelSO<T> : ScriptableObject
    {
        private event Action<T> _event;

        public void Subscribe(Action<T> handler) => _event += handler;
        public void Unsubscribe(Action<T> handler) => _event -= handler;
        public void Raise(T eventData) => _event?.Invoke(eventData);
        public void Clear() => _event = null;
    }
}
