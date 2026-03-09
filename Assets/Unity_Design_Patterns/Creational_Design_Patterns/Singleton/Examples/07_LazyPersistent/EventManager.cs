using UnityEngine;
using Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts;

namespace Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts.Examples
{
    /// <summary>
    /// An example service that inherits <see cref="LazyPersistentSingleton{T}"/>.
    /// Requires no manual scene setup. The first call to Instance creates the
    /// GameObject automatically if it does not already exist.
    /// </summary>
    public class EventManager : LazyPersistentSingleton<EventManager>
    {
        public void Trigger(string eventName)
        {
            Debug.Log($"[EventManager] Event triggered: {eventName}");
        }
    }
}
