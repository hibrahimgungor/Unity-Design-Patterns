using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_05_EventChannel._01_CSharpEvent
{
    /// <summary>
    /// Concrete subscriber that listens to quest events via the EventChannel.
    /// Holds no reference to QuestSystem — subscribes directly by event type.
    /// </summary>
    public class QuestAudio : MonoBehaviour
    {
        private void OnEnable()
        {
            EventChannel<QuestStartedEvent>.Subscribe(HandleQuestStarted);
            EventChannel<QuestCompletedEvent>.Subscribe(HandleQuestCompleted);
        }

        private void OnDisable()
        {
            EventChannel<QuestStartedEvent>.Unsubscribe(HandleQuestStarted);
            EventChannel<QuestCompletedEvent>.Unsubscribe(HandleQuestCompleted);
        }

        private void HandleQuestStarted(QuestStartedEvent e) => Debug.Log("QuestAudio: Playing quest start sound.");
        private void HandleQuestCompleted(QuestCompletedEvent e) => Debug.Log("QuestAudio: Playing quest complete sound.");
    }
}
