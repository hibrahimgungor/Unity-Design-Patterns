using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_02_CSharpEvent
{
    /// <summary>
    /// Concrete observer that represents the audio layer.
    /// Subscribes only to the events it cares about — start and completion sounds.
    /// Attach to a GameObject and assign the QuestSystem reference via the Inspector.
    /// </summary>
    public class QuestAudio : MonoBehaviour
    {
        [SerializeField] private QuestSystem _questSystem;

        private void OnEnable()
        {
            _questSystem.OnQuestStarted += HandleQuestStarted;
            _questSystem.OnQuestCompleted += HandleQuestCompleted;
        }

        private void OnDisable()
        {
            _questSystem.OnQuestStarted -= HandleQuestStarted;
            _questSystem.OnQuestCompleted -= HandleQuestCompleted;
        }

        private void HandleQuestStarted() => Debug.Log("QuestAudio: Playing quest start sound.");
        private void HandleQuestCompleted(QuestData data) => Debug.Log("QuestAudio: Playing quest complete sound.");
    }
}
