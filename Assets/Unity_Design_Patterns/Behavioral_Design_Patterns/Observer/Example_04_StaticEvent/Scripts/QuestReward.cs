using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_04_StaticEvent
{
    /// <summary>
    /// Concrete observer that subscribes to static QuestSystem events.
    /// No Inspector reference required — subscription is made directly via the class name.
    /// Receives notifications from every QuestSystem instance in the scene.
    /// </summary>
    public class QuestReward : MonoBehaviour
    {
        private void OnEnable() => QuestSystem.OnQuestCompleted += HandleQuestCompleted;
        private void OnDisable() => QuestSystem.OnQuestCompleted -= HandleQuestCompleted;

        private void HandleQuestCompleted(QuestData data) => Debug.Log($"QuestReward [{name}]: Granting {data.RewardXP} XP for quest '{data.QuestName}'.");
    }
}
