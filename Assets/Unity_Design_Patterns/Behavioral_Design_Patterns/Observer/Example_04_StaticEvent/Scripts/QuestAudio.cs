using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_04_StaticEvent
{
    /// <summary>
    /// Concrete observer that subscribes to static QuestSystem events.
    /// No Inspector reference required — subscription is made directly via the class name.
    /// Receives notifications from every QuestSystem instance in the scene.
    /// </summary>
    public class QuestAudio : MonoBehaviour
    {
        private void OnEnable()
        {
            QuestSystem.OnQuestStarted += HandleQuestStarted;
            QuestSystem.OnQuestCompleted += HandleQuestCompleted;
        }

        private void OnDisable()
        {
            QuestSystem.OnQuestStarted -= HandleQuestStarted;
            QuestSystem.OnQuestCompleted -= HandleQuestCompleted;
        }

        private void HandleQuestStarted() => Debug.Log($"QuestAudio [{name}]: Playing quest start sound.");
        private void HandleQuestCompleted(QuestData data) => Debug.Log($"QuestAudio [{name}]: Playing quest complete sound.");
    }
}
