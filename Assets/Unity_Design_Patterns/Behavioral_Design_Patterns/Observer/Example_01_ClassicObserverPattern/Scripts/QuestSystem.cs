using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_01_ClassicObserverPattern
{
    /// <summary>
    /// Coordinates quest events by delegating to dedicated subject instances.
    ///
    /// Each event type has its own subject — QuestStartedSubject, QuestCompletedSubject,
    /// QuestFailedSubject. This is the purest form of the classic Observer pattern:
    /// one subject per meaningful occurrence, no shared state, no event type flags.
    ///
    /// Observers register directly with the subject they care about. QuestReward only
    /// registers with QuestCompletedSubject — it never receives start or fail notifications.
    ///
    /// Scene setup:
    /// Attach this component to a GameObject alongside QuestStartedSubject,
    /// QuestCompletedSubject, and QuestFailedSubject. Assign each subject reference
    /// in the Inspector. Observer components (QuestUI, QuestAudio, QuestReward) must
    /// also hold references to the subjects they want to observe.
    /// </summary>
    public class QuestSystem : MonoBehaviour
    {
        [SerializeField] private QuestStartedSubject _questStartedSubject;
        [SerializeField] private QuestCompletedSubject _questCompletedSubject;
        [SerializeField] private QuestFailedSubject _questFailedSubject;

        public void StartQuest()
        {
            Debug.Log("QuestSystem: Quest started.");
            _questStartedSubject.NotifyObservers();
        }

        public void CompleteQuest(QuestData data)
        {
            Debug.Log("QuestSystem: Quest completed.");
            _questCompletedSubject.Raise(data);
        }

        public void FailQuest(int questId)
        {
            Debug.Log("QuestSystem: Quest failed.");
            _questFailedSubject.Raise(questId);
        }
    }
}
