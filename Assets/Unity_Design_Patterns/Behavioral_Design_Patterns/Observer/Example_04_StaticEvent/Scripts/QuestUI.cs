using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_04_StaticEvent
{
    /// <summary>
    /// Concrete observer that subscribes to static QuestSystem events.
    /// No Inspector reference required — subscription is made directly via the class name.
    /// Receives notifications from every QuestSystem instance in the scene.
    /// </summary>
    public class QuestUI : MonoBehaviour
    {
        private void OnEnable()
        {
            QuestSystem.OnQuestStarted += HandleQuestStarted;
            QuestSystem.OnQuestCompleted += HandleQuestCompleted;
            QuestSystem.OnQuestFailed += HandleQuestFailed;
        }

        private void OnDisable()
        {
            QuestSystem.OnQuestStarted -= HandleQuestStarted;
            QuestSystem.OnQuestCompleted -= HandleQuestCompleted;
            QuestSystem.OnQuestFailed -= HandleQuestFailed;
        }

        private void HandleQuestStarted() => Debug.Log($"QuestUI [{name}]: Showing quest started screen.");
        private void HandleQuestCompleted(QuestData data) => Debug.Log($"QuestUI [{name}]: Quest '{data.QuestName}' completed. Earned {data.RewardXP} XP.");
        private void HandleQuestFailed(int questId) => Debug.Log($"QuestUI [{name}]: Showing quest failed screen. Quest ID: {questId}");
    }
}
