using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_02_CSharpEvent
{
    /// <summary>
    /// Concrete observer that represents the UI layer.
    /// Subscribes to all three quest events and responds independently to each.
    /// Attach to a GameObject and assign the QuestSystem reference via the Inspector.
    /// </summary>
    public class QuestUI : MonoBehaviour
    {
        [SerializeField] private QuestSystem _questSystem;

        private void OnEnable()
        {
            _questSystem.OnQuestStarted += HandleQuestStarted;
            _questSystem.OnQuestCompleted += HandleQuestCompleted;
            _questSystem.OnQuestFailed += HandleQuestFailed;
        }

        private void OnDisable()
        {
            _questSystem.OnQuestStarted -= HandleQuestStarted;
            _questSystem.OnQuestCompleted -= HandleQuestCompleted;
            _questSystem.OnQuestFailed -= HandleQuestFailed;
        }

        private void HandleQuestStarted() => Debug.Log("QuestUI: Showing quest started screen.");
        private void HandleQuestCompleted(QuestData data) => Debug.Log($"QuestUI: Quest '{data.QuestName}' completed. Earned {data.RewardXP} XP.");
        private void HandleQuestFailed(int questId) => Debug.Log($"QuestUI: Showing quest failed screen. Quest ID: {questId}");
    }
}
