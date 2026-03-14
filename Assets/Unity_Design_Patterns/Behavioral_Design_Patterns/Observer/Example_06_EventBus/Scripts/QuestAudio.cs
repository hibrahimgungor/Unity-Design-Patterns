using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_06_EventBus
{
    /// <summary>
    /// Concrete subscriber that listens to quest events via the EventBus.
    /// Holds no reference to QuestSystem — subscribes directly by event type.
    /// </summary>
    public class QuestAudio : MonoBehaviour
    {
        private void OnEnable()
        {
            EventBus.Subscribe<QuestStartedEvent>(HandleQuestStarted);
            EventBus.Subscribe<QuestCompletedEvent>(HandleQuestCompleted);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<QuestStartedEvent>(HandleQuestStarted);
            EventBus.Unsubscribe<QuestCompletedEvent>(HandleQuestCompleted);
        }

        private void HandleQuestStarted(QuestStartedEvent e) => Debug.Log("QuestAudio: Playing quest start sound.");
        private void HandleQuestCompleted(QuestCompletedEvent e) => Debug.Log("QuestAudio: Playing quest complete sound.");
    }
}
