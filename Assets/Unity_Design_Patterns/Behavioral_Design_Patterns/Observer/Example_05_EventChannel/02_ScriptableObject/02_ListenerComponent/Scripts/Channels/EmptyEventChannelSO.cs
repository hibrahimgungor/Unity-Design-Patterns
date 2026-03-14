using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_05_EventChannel._02_ScriptableObject._02_ListenerComponent
{
    /// <summary>
    /// Concrete event channel for events that carry no data.
    ///
    /// The name reflects the data type — EmptyEventChannelSO signals that this channel
    /// is a notification only. No payload is delivered to subscribers.
    ///
    /// This naming convention is what makes the ScriptableObject approach scale cleanly.
    /// Each new data type needs exactly one concrete channel class:
    /// IntEventChannelSO, FloatEventChannelSO, Vector3EventChannelSO, and so on.
    /// The asset name in the Project window (e.g. QuestStartedEvent, QuestFailedEvent)
    /// identifies what the channel represents — the class identifies what it carries.
    ///
    /// Usage:
    /// - Create an asset via Assets > Create > Events > EmptyEventChannel
    /// - Name the asset after the event it represents (e.g. QuestStartedEvent)
    /// - Assign the asset to both the publisher and any listeners
    /// </summary>
    [CreateAssetMenu(
        fileName = "NewEmptyEventChannel",
        menuName = "Example05/02_ListenerComponent/Events/EmptyEventChannel")]
    public class EmptyEventChannelSO : BaseEventChannelSO<Empty> { }

    /// <summary>
    /// A zero-sized struct used as the type parameter for EmptyEventChannelSO.
    ///
    /// Why does this exist?
    /// BaseEventChannelSO&lt;T&gt; is generic — it requires a concrete type for T.
    /// C# does not allow Action&lt;void&gt;, so there is no way to express
    /// "a channel that carries no data" without providing a type argument.
    ///
    /// The alternative would be a separate non-generic BaseEventChannelSO base class,
    /// which would mean maintaining two parallel class hierarchies. Empty is the
    /// deliberate trade-off: one base class, one extra struct.
    ///
    /// In handler signatures, the convention is to use the discard parameter:
    ///     private void HandleQuestStarted(Empty _) => ...
    /// The underscore signals that the parameter is intentionally ignored.
    /// </summary>
    public struct Empty { }
}
