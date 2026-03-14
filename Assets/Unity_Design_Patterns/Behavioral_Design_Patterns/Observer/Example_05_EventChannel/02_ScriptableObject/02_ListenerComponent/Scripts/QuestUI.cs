using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_05_EventChannel._02_ScriptableObject._02_ListenerComponent
{
    /// <summary>
    /// Reacts to quest events by updating the UI.
    ///
    /// Methods are public so they can be wired to an EventListener's UnityEvent
    /// in the Inspector. This class holds no channel references and no subscription
    /// code — all wiring is done via the EmptyEventListener and IntEventListener
    /// components in the scene.
    /// </summary>
    public class QuestUI : MonoBehaviour
    {
        public void OnQuestStarted(Empty _) => Debug.Log("QuestUI: Showing quest started screen.");
        public void OnQuestCompleted(QuestData data) => Debug.Log($"QuestUI: Quest '{data.QuestName}' completed. Earned {data.RewardXP} XP.");
        public void OnQuestFailed(int questId) => Debug.Log($"QuestUI: Showing quest failed screen. Quest ID: {questId}");
    }
}
