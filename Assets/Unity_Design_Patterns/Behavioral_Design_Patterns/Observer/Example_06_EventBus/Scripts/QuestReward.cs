using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_06_EventBus
{
    /// <summary>
    /// Concrete subscriber that listens to quest completion via the EventBus.
    /// Holds no reference to QuestSystem — subscribes directly by event type.
    /// </summary>
    public class QuestReward : MonoBehaviour
    {
        private void OnEnable() => EventBus.Subscribe<QuestCompletedEvent>(HandleQuestCompleted);
        private void OnDisable() => EventBus.Unsubscribe<QuestCompletedEvent>(HandleQuestCompleted);

        private void HandleQuestCompleted(QuestCompletedEvent e) =>
            Debug.Log($"QuestReward: Granting {e.RewardXP} XP for quest '{e.QuestName}'.");
    }
}
