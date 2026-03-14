using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_05_EventChannel._02_ScriptableObject._01_DirectSubscription
{
    /// <summary>
    /// Concrete subscriber that listens to quest events via ScriptableObject event channels.
    ///
    /// Each channel asset is assigned in the Inspector. Holds no reference to QuestSystem
    /// or any other subscriber — both sides are decoupled through the shared channel assets.
    /// </summary>
    public class QuestUI : MonoBehaviour
    {
        [SerializeField] private EmptyEventChannelSO _questStartedChannel;
        [SerializeField] private QuestDataEventChannelSO _questCompletedChannel;
        [SerializeField] private IntEventChannelSO _questFailedChannel;

        private void OnEnable()
        {
            _questStartedChannel.Subscribe(HandleQuestStarted);
            _questCompletedChannel.Subscribe(HandleQuestCompleted);
            _questFailedChannel.Subscribe(HandleQuestFailed);
        }

        private void OnDisable()
        {
            _questStartedChannel.Unsubscribe(HandleQuestStarted);
            _questCompletedChannel.Unsubscribe(HandleQuestCompleted);
            _questFailedChannel.Unsubscribe(HandleQuestFailed);
        }

        private void HandleQuestStarted(Empty _) => Debug.Log("QuestUI: Showing quest started screen.");
        private void HandleQuestCompleted(QuestData data) => Debug.Log($"QuestUI: Quest '{data.QuestName}' completed. Earned {data.RewardXP} XP.");
        private void HandleQuestFailed(int questId) => Debug.Log($"QuestUI: Showing quest failed screen. Quest ID: {questId}");
    }
}
