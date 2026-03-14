using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_00_NoPattern._02_Polling
{
    /// <summary>
    /// Polls QuestSystem state every frame and reacts when a change is detected.
    /// Requires a direct reference to QuestSystem via the Inspector.
    ///
    /// Note that ResetFlags() must be called after reacting — otherwise this observer
    /// will trigger again on the next frame. In a multi-observer setup, the reset
    /// timing becomes a coordination problem: who resets, and when?
    /// </summary>
    public class QuestUI : MonoBehaviour
    {
        [SerializeField] private QuestSystem _questSystem;

        private void Update()
        {
            if (_questSystem.QuestStarted)
                Debug.Log("QuestUI: Showing quest started screen.");

            if (_questSystem.QuestCompleted)
                Debug.Log($"QuestUI: Quest '{_questSystem.LastQuestData.QuestName}' completed. Earned {_questSystem.LastQuestData.RewardXP} XP.");

            if (_questSystem.QuestFailed)
                Debug.Log($"QuestUI: Showing quest failed screen. Quest ID: {_questSystem.LastFailedQuestId}");
        }
    }
}
