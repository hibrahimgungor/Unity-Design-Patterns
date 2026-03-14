using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_05_EventChannel._02_ScriptableObject._01_DirectSubscription
{
    /// <summary>
    /// Concrete subscriber that listens to quest completion via ScriptableObject event channel.
    ///
    /// Uses the int Value carried by IntEvent to grant the correct reward amount.
    /// The channel asset is assigned in the Inspector. Holds no reference to QuestSystem
    /// or any other subscriber — both sides are decoupled through the shared channel asset.
    /// </summary>
    public class QuestReward : MonoBehaviour
    {
        [SerializeField] private QuestDataEventChannelSO _questCompletedChannel;

        private void OnEnable() => _questCompletedChannel.Subscribe(HandleQuestCompleted);
        private void OnDisable() => _questCompletedChannel.Unsubscribe(HandleQuestCompleted);

        private void HandleQuestCompleted(QuestData data) => Debug.Log($"QuestReward: Granting {data.RewardXP} XP for quest '{data.QuestName}'.");
    }
}
