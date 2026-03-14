using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_06_EventBus
{
    /// <summary>
    /// Concrete subscriber that listens to quest events via the EventBus.
    /// Holds no reference to QuestSystem — subscribes directly by event type.
    /// </summary>
    public class QuestUI : MonoBehaviour
    {
        private void OnEnable()
        {
            EventBus.Subscribe<QuestStartedEvent>(HandleQuestStarted);
            EventBus.Subscribe<QuestCompletedEvent>(HandleQuestCompleted);
            EventBus.Subscribe<QuestFailedEvent>(HandleQuestFailed);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<QuestStartedEvent>(HandleQuestStarted);
            EventBus.Unsubscribe<QuestCompletedEvent>(HandleQuestCompleted);
            EventBus.Unsubscribe<QuestFailedEvent>(HandleQuestFailed);
        }

        private void HandleQuestStarted(QuestStartedEvent e) => Debug.Log("QuestUI: Showing quest started screen.");
        private void HandleQuestCompleted(QuestCompletedEvent e) => Debug.Log($"QuestUI: Quest '{e.QuestName}' completed. Earned {e.RewardXP} XP.");
        private void HandleQuestFailed(QuestFailedEvent e) => Debug.Log($"QuestUI: Showing quest failed screen. Quest ID: {e.QuestId}");
    }
}
