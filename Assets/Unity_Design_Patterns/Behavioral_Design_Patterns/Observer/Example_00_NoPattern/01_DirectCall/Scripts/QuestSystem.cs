using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_00_NoPattern._01_DirectCall
{
    /// <summary>
    /// QuestSystem without any pattern applied.
    ///
    /// This class directly calls methods on every system that needs to react
    /// to a quest event. To add a new reaction — an analytics system, a particle
    /// effect, a save system — QuestSystem itself must be modified.
    ///
    /// This violates the Open/Closed Principle: the class is not closed for
    /// modification. Every new dependent reopens it.
    ///
    /// It also violates the Single Responsibility Principle: QuestSystem is
    /// responsible both for quest logic and for knowing which systems react to it.
    ///
    /// This tight coupling is exactly what the Observer pattern is designed to remove.
    /// </summary>
    public class QuestSystem : MonoBehaviour
    {
        [SerializeField] private QuestUI _questUI;
        [SerializeField] private QuestAudio _questAudio;
        [SerializeField] private QuestReward _questReward;

        public void StartQuest()
        {
            Debug.Log("QuestSystem: Quest started.");

            _questUI.OnQuestStarted();
            _questAudio.OnQuestStarted();
        }

        public void CompleteQuest(QuestData data)
        {
            Debug.Log("QuestSystem: Quest completed.");

            _questUI.OnQuestCompleted(data);
            _questAudio.OnQuestCompleted(data);
            _questReward.OnQuestCompleted(data);
        }

        public void FailQuest(int questId)
        {
            Debug.Log("QuestSystem: Quest failed.");

            _questUI.OnQuestFailed(questId);
        }
    }
}
