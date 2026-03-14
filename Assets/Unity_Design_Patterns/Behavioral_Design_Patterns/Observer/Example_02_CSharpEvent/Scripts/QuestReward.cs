using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_02_CSharpEvent
{
    /// <summary>
    /// Concrete observer that represents the reward layer.
    /// Subscribes only to quest completion — rewards are granted on success only.
    /// Attach to a GameObject and assign the QuestSystem reference via the Inspector.
    /// </summary>
    public class QuestReward : MonoBehaviour
    {
        [SerializeField] private QuestSystem _questSystem;

        private void OnEnable() => _questSystem.OnQuestCompleted += HandleQuestCompleted;
        private void OnDisable() => _questSystem.OnQuestCompleted -= HandleQuestCompleted;

        private void HandleQuestCompleted(QuestData data) => Debug.Log($"QuestReward: Granting {data.RewardXP} XP for quest '{data.QuestName}'.");
    }
}
