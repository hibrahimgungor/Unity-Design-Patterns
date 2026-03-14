using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_05_EventChannel._02_ScriptableObject._01_DirectSubscription
{
    /// <summary>
    /// Concrete event channel for events that carry a single integer value.
    ///
    /// The name reflects the data type — IntEventChannelSO signals that this channel
    /// delivers an int payload to all subscribers when raised.
    ///
    /// Usage:
    /// - Create an asset via Assets > Create > Events > IntEventChannel
    /// - Name the asset after the event it represents (e.g. QuestCompletedEvent)
    /// - Assign the asset to both the publisher and any subscribers
    /// - The publisher calls Raise(value) — subscribers receive the int directly
    /// </summary>
    [CreateAssetMenu(
        fileName = "NewIntEventChannel",
        menuName = "Example05/01_DirectSubscription/Events/IntEventChannel")]
    public class IntEventChannelSO : BaseEventChannelSO<int> { }
}
