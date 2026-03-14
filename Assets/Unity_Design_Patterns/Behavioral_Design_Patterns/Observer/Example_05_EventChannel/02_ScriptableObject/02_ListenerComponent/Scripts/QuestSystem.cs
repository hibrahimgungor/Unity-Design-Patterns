using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_05_EventChannel._02_ScriptableObject._02_ListenerComponent
{
    /// <summary>
    /// Concrete publisher that raises events through ScriptableObject event channels.
    ///
    /// Holds no reference to any listener — complete decoupling from observers.
    /// Each channel asset is assigned in the Inspector. Any system in the project
    /// can listen to a channel without this class knowing about it.
    /// </summary>
    public class QuestSystem : MonoBehaviour
    {
        [SerializeField] private EmptyEventChannelSO _questStartedChannel;
        [SerializeField] private QuestDataEventChannelSO _questCompletedChannel;
        [SerializeField] private IntEventChannelSO _questFailedChannel;

        public void StartQuest()
        {
            Debug.Log("QuestSystem: Quest started.");
            _questStartedChannel.Raise(new Empty());
        }

        public void CompleteQuest(QuestData data)
        {
            Debug.Log("QuestSystem: Quest completed.");
            _questCompletedChannel.Raise(data);
        }

        public void FailQuest(int questId)
        {
            Debug.Log("QuestSystem: Quest failed.");
            _questFailedChannel.Raise(questId);
        }
    }
}
