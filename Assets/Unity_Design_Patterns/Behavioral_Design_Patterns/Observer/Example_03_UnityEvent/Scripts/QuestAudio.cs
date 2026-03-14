using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_03_UnityEvent
{
    /// <summary>
    /// Concrete observer that subscribes as a runtime listener via AddListener().
    /// Functionally equivalent to the C# event += syntax from Example 02.
    /// Runtime listeners are not serialized and must be removed manually via RemoveListener().
    /// Attach to a GameObject and assign the QuestSystem reference via the Inspector.
    /// </summary>
    public class QuestAudio : MonoBehaviour
    {
        [SerializeField] private QuestSystem _questSystem;

        private void OnEnable()
        {
            _questSystem.OnQuestStarted.AddListener(HandleQuestStarted);
            _questSystem.OnQuestCompleted.AddListener(HandleQuestCompleted);
        }

        private void OnDisable()
        {
            _questSystem.OnQuestStarted.RemoveListener(HandleQuestStarted);
            _questSystem.OnQuestCompleted.RemoveListener(HandleQuestCompleted);
        }

        private void HandleQuestStarted() => Debug.Log("QuestAudio: Playing quest start sound.");
        private void HandleQuestCompleted(QuestData data) => Debug.Log("QuestAudio: Playing quest complete sound.");
    }
}
