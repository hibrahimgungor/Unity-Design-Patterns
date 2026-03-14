using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_05_EventChannel._02_ScriptableObject._01_DirectSubscription
{
    /// <summary>
    /// Concrete event channel for events that carry quest completion data.
    ///
    /// Demonstrates that the ScriptableObject channel approach scales naturally
    /// to custom data types — not just primitives. Any serializable type can serve
    /// as the channel's payload.
    ///
    /// Usage:
    /// - Create an asset via Assets > Create > Events > QuestDataEventChannel
    /// - Name the asset after the event it represents (e.g. QuestCompletedEvent)
    /// - Assign the asset to both the publisher and any subscribers
    /// </summary>
    [CreateAssetMenu(
        fileName = "NewQuestDataEventChannel",
        menuName = "Example05/01_DirectSubscription/Events/QuestDataEventChannel")]
    public class QuestDataEventChannelSO : BaseEventChannelSO<QuestData> { }
}
