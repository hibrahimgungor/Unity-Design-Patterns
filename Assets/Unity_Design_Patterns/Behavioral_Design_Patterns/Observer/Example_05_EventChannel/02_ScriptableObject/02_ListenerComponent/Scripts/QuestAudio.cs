using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_05_EventChannel._02_ScriptableObject._02_ListenerComponent
{
    /// <summary>
    /// Reacts to quest events by playing audio.
    ///
    /// Methods are public so they can be wired to an EventListener's UnityEvent
    /// in the Inspector. This class holds no channel references and no subscription
    /// code — all wiring is done via the EmptyEventListener and IntEventListener
    /// components in the scene.
    /// </summary>
    public class QuestAudio : MonoBehaviour
    {
        public void OnQuestStarted(Empty _) => Debug.Log("QuestAudio: Playing quest start sound.");
        public void OnQuestCompleted(QuestData data) => Debug.Log("QuestAudio: Playing quest complete sound.");
    }
}
