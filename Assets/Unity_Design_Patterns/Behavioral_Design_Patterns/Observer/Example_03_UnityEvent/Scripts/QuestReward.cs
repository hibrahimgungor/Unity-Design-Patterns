using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_03_UnityEvent
{
    /// <summary>
    /// Concrete observer that subscribes as a runtime listener via AddListener().
    /// Attach to a GameObject and assign the QuestSystem reference via the Inspector.
    /// </summary>
    public class QuestReward : MonoBehaviour
    {
        [SerializeField] private QuestSystem _questSystem;

        private void OnEnable() => _questSystem.OnQuestCompleted.AddListener(HandleQuestCompleted);
        private void OnDisable() => _questSystem.OnQuestCompleted.RemoveListener(HandleQuestCompleted);

        private void HandleQuestCompleted(QuestData data) => Debug.Log($"QuestReward: Granting {data.RewardXP} XP for quest '{data.QuestName}'.");
    }
}
