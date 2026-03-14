using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_00_NoPattern._01_DirectCall
{
    /// <summary>
    /// Reacts to quest events via direct method calls from QuestSystem.
    /// QuestSystem holds a reference to this class and calls its methods explicitly.
    /// </summary>
    public class QuestAudio : MonoBehaviour
    {
        public void OnQuestStarted() => Debug.Log("QuestAudio: Playing quest start sound.");
        public void OnQuestCompleted(QuestData data) => Debug.Log("QuestAudio: Playing quest complete sound.");
    }
}
