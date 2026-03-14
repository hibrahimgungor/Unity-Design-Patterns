using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_03_UnityEvent
{
    /// <summary>
    /// Concrete observer that subscribes as a persistent listener via the Inspector.
    /// No subscription code required — assign HandleQuestStarted, HandleQuestCompleted,
    /// and HandleQuestFailed to the corresponding UnityEvents on QuestSystem directly
    /// in the Inspector.
    ///
    /// Because this is a persistent listener, the binding is serialized with the scene.
    /// Renaming any of these methods will silently break the Inspector binding.
    /// </summary>
    public class QuestUI : MonoBehaviour
    {
        public void HandleQuestStarted() => Debug.Log("QuestUI: Showing quest started screen.");
        public void HandleQuestCompleted(QuestData data) => Debug.Log($"QuestUI: Quest '{data.QuestName}' completed. Earned {data.RewardXP} XP.");
        public void HandleQuestFailed(int questId) => Debug.Log($"QuestUI: Showing quest failed screen. Quest ID: {questId}");
    }
}
