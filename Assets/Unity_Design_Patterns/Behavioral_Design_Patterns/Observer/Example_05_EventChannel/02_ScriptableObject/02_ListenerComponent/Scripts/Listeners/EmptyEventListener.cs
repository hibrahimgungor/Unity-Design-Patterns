using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_05_EventChannel._02_ScriptableObject._02_ListenerComponent
{
    /// <summary>
    /// Concrete event listener for channels that carry no data.
    ///
    /// Attach this component to any GameObject that needs to react to an
    /// EmptyEventChannelSO. Assign the channel asset and wire up the UnityEvent
    /// in the Inspector — no subscription code required.
    /// </summary>
    public class EmptyEventListener : BaseEventListenerSO<Empty> { }
}
