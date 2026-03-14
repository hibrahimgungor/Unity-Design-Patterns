using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_00_NoPattern._01_DirectCall
{
    /// <summary>
    /// Reacts to quest events via direct method calls from QuestSystem.
    /// QuestSystem holds a reference to this class and calls its methods explicitly.
    /// </summary>
    public class QuestUI : MonoBehaviour
    {
        public void OnQuestStarted() => Debug.Log("QuestUI: Showing quest started screen.");
        public void OnQuestCompleted(QuestData data) => Debug.Log($"QuestUI: Quest '{data.QuestName}' completed. Earned {data.RewardXP} XP.");
        public void OnQuestFailed(int questId) => Debug.Log($"QuestUI: Showing quest failed screen. Quest ID: {questId}");
    }
}
