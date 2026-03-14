using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_05_EventChannel._01_CSharpEvent
{
    /// <summary>
    /// Concrete subscriber that listens to quest events via the EventChannel.
    /// Holds no reference to QuestSystem — subscribes directly by event type.
    /// </summary>
    public class QuestUI : MonoBehaviour
    {
        private void OnEnable()
        {
            EventChannel<QuestStartedEvent>.Subscribe(HandleQuestStarted);
            EventChannel<QuestCompletedEvent>.Subscribe(HandleQuestCompleted);
            EventChannel<QuestFailedEvent>.Subscribe(HandleQuestFailed);
        }

        private void OnDisable()
        {
            EventChannel<QuestStartedEvent>.Unsubscribe(HandleQuestStarted);
            EventChannel<QuestCompletedEvent>.Unsubscribe(HandleQuestCompleted);
            EventChannel<QuestFailedEvent>.Unsubscribe(HandleQuestFailed);
        }

        private void HandleQuestStarted(QuestStartedEvent e) => Debug.Log("QuestUI: Showing quest started screen.");
        private void HandleQuestCompleted(QuestCompletedEvent e) => Debug.Log($"QuestUI: Quest '{e.QuestName}' completed. Earned {e.RewardXP} XP.");
        private void HandleQuestFailed(QuestFailedEvent e) => Debug.Log($"QuestUI: Showing quest failed screen. Quest ID: {e.QuestId}");
    }
}
