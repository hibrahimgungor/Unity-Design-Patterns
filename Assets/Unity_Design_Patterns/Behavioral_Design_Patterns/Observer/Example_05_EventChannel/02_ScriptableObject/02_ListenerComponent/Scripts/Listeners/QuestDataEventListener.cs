using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_05_EventChannel._02_ScriptableObject._02_ListenerComponent
{
    /// <summary>
    /// Concrete event listener for channels that carry QuestData.
    ///
    /// Attach this component to any GameObject that needs to react to a
    /// QuestDataEventChannelSO. Assign the channel asset and wire up the UnityEvent
    /// in the Inspector — no subscription code required.
    /// </summary>
    public class QuestDataEventListener : BaseEventListenerSO<QuestData> { }
}
