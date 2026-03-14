using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_05_EventChannel._01_CSharpEvent
{
    /// <summary>
    /// Concrete subscriber that listens to quest completion via the EventChannel.
    /// Uses the data carried by QuestCompletedEvent to grant the correct reward.
    /// Holds no reference to QuestSystem — subscribes directly by event type.
    /// </summary>
    public class QuestReward : MonoBehaviour
    {
        private void OnEnable() => EventChannel<QuestCompletedEvent>.Subscribe(HandleQuestCompleted);
        private void OnDisable() => EventChannel<QuestCompletedEvent>.Unsubscribe(HandleQuestCompleted);

        private void HandleQuestCompleted(QuestCompletedEvent e) =>
            Debug.Log($"QuestReward: Granting {e.RewardXP} XP for quest '{e.QuestName}'.");
    }
}
