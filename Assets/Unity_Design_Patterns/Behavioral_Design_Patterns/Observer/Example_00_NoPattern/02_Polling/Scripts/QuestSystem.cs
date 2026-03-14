using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_00_NoPattern._02_Polling
{
    /// <summary>
    /// QuestSystem that exposes its state as public properties.
    ///
    /// Instead of notifying dependents directly, this class simply updates its
    /// internal state. Each dependent is responsible for checking that state
    /// every frame in its own Update() loop — this is called polling.
    ///
    /// Polling works but has two significant costs:
    /// - Every polling observer runs every frame, regardless of whether anything changed.
    ///   At scale this becomes a performance concern.
    /// - State must be carefully reset after being read, or observers will react
    ///   to the same change multiple times. This reset logic is easy to get wrong.
    ///
    /// The Observer pattern replaces polling with a push model: the subject notifies
    /// observers exactly once when a change occurs, with no per-frame overhead.
    /// </summary>
    public class QuestSystem : MonoBehaviour
    {
        public bool QuestStarted { get; private set; }
        public bool QuestCompleted { get; private set; }
        public bool QuestFailed { get; private set; }
        public QuestData LastQuestData { get; private set; }
        public int LastFailedQuestId { get; private set; }

        public void StartQuest()
        {
            Debug.Log("QuestSystem: Quest started.");
            QuestStarted = true;
        }

        public void CompleteQuest(QuestData data)
        {
            Debug.Log("QuestSystem: Quest completed.");
            LastQuestData = data;
            QuestCompleted = true;
        }

        public void FailQuest(int questId)
        {
            Debug.Log("QuestSystem: Quest failed.");
            LastFailedQuestId = questId;
            QuestFailed = true;
        }

        public void ResetFlags()
        {
            QuestStarted = false;
            QuestCompleted = false;
            QuestFailed = false;
        }
    }
}
