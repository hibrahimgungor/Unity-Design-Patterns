using System;
using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_04_StaticEvent
{
    /// <summary>
    /// Concrete subject that uses static events instead of instance events.
    ///
    /// Static events are shared across all instances of the class. Any QuestSystem
    /// in the scene that calls CompleteQuest will notify every subscriber — regardless
    /// of which instance fired the event. Observers have no way to determine which
    /// QuestSystem triggered the notification.
    ///
    /// When this works well:
    /// A single QuestSystem in the scene. One publisher, many listeners.
    /// Static events remove the need for Inspector references entirely —
    /// observers subscribe by class name, not by instance.
    ///
    /// When this breaks:
    /// Two or more QuestSystem instances in the scene. Quest1 completing triggers
    /// all subscribers — including those that belong to Quest2. The event carries
    /// no identity, so there is no way to filter by source.
    ///
    /// Other risks:
    /// - Subscribers that forget to unsubscribe remain registered for the application
    ///   lifetime. Static events are never garbage collected as long as subscribers exist.
    /// - Harder to test in isolation — the static event is global state.
    ///
    /// Scene setup:
    /// Add two QuestSystem GameObjects to the scene and assign different names.
    /// Press Space and C to trigger each. Observe that both sets of observers
    /// respond to every notification regardless of which instance fired it.
    /// </summary>
    public class QuestSystem : MonoBehaviour
    {
        public static event Action OnQuestStarted;
        public static event Action<QuestData> OnQuestCompleted;
        public static event Action<int> OnQuestFailed;

        public void StartQuest()
        {
            Debug.Log($"QuestSystem [{name}]: Quest started.");
            OnQuestStarted?.Invoke();
        }

        public void CompleteQuest(QuestData data)
        {
            Debug.Log($"QuestSystem [{name}]: Quest completed.");
            OnQuestCompleted?.Invoke(data);
        }

        public void FailQuest(int questId)
        {
            Debug.Log($"QuestSystem [{name}]: Quest failed.");
            OnQuestFailed?.Invoke(questId);
        }
    }
}
