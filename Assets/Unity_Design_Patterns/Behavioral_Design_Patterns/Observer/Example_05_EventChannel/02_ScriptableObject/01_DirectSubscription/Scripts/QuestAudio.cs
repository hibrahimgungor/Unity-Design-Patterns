using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_05_EventChannel._02_ScriptableObject._01_DirectSubscription
{
    /// <summary>
    /// Concrete subscriber that listens to quest events via ScriptableObject event channels.
    ///
    /// Each channel asset is assigned in the Inspector. Holds no reference to QuestSystem
    /// or any other subscriber — both sides are decoupled through the shared channel assets.
    /// </summary>
    public class QuestAudio : MonoBehaviour
    {
        [SerializeField] private EmptyEventChannelSO _questStartedChannel;
        [SerializeField] private QuestDataEventChannelSO _questCompletedChannel;

        private void OnEnable()
        {
            _questStartedChannel.Subscribe(HandleQuestStarted);
            _questCompletedChannel.Subscribe(HandleQuestCompleted);
        }

        private void OnDisable()
        {
            _questStartedChannel.Unsubscribe(HandleQuestStarted);
            _questCompletedChannel.Unsubscribe(HandleQuestCompleted);
        }

        private void HandleQuestStarted(Empty _) => Debug.Log("QuestAudio: Playing quest start sound.");
        private void HandleQuestCompleted(QuestData data) => Debug.Log("QuestAudio: Playing quest complete sound.");
    }
}
