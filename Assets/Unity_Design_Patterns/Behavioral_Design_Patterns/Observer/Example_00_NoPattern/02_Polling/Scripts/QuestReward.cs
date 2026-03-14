using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_00_NoPattern._02_Polling
{
    /// <summary>
    /// Polls QuestSystem state every frame and reacts when a change is detected.
    /// Requires a direct reference to QuestSystem via the Inspector.
    /// </summary>
    public class QuestReward : MonoBehaviour
    {
        [SerializeField] private QuestSystem _questSystem;

        private void Update()
        {
            if (_questSystem.QuestCompleted)
                Debug.Log($"QuestReward: Granting {_questSystem.LastQuestData.RewardXP} XP for quest '{_questSystem.LastQuestData.QuestName}'.");
        }
    }
}
